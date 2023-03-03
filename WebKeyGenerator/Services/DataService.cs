using Microsoft.EntityFrameworkCore;
using PetHelper.Models.Buisness;
using System.Globalization;
using System.IO.Compression;
using WebKeyGenerator.Context;
using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Identity;
using WebKeyGenerator.Models.Requests;
using WebKeyGenerator.Utils;

namespace WebKeyGenerator.Services
{
    public class DataService : IDataService
    {

        private readonly AppDbContext db;
        private Logger logger = new WebKeyGenerator.Utils.Logger();


        public DataService(AppDbContext db)
        {
            this.db = db;
        }

        #region admin
        public void AddOrUpdateUser(User u)
        {
            logger.Trace($"AddOurUpdateUser: {u.ToJson()}");
            if (string.IsNullOrEmpty(u.Login)
                || string.IsNullOrEmpty(u.Email))
            {
                logger.Error($"Не заполнено одно или несколько обязательных полей");
                throw new Exception($"Не заполнено одно или несколько обязательных полей");
            }


            if (u.Id == -1)//Костыль для хотфикса
                u = new User()
                {
                    Active = u.Active,
                    Email = u.Email,
                    Login = u.Login,
                    Password = u.Password,
                    PasswordSalt = u.PasswordSalt,
                    RefreshTokens = u.RefreshTokens,
                    Role = u.Role
                };

            var user = db.Users.Include(e => e.RefreshTokens).FirstOrDefault(e => e.Id == u.Id);
            if (user == null)
            {
                if (db.Users.Any(e => e.Login.ToLower() == u.Login.ToLower()))
                    throw new Exception($"Пользователь с логином {u.Login} уже есть в системе");
                if (db.Users.Any(e => e.Email.ToLower() == u.Email.ToLower()))
                    throw new Exception($"Пользователь с Email {u.Email} уже есть в системе");

                if (string.IsNullOrEmpty(u.Password) || u.Password.Length < 6)
                {
                    throw new Exception($"Длина пароля должна быть минимум 6 символов");
                }
                var salt = PasswordHelper.GetSecureSalt();
                var passwordHash = PasswordHelper.HashUsingPbkdf2(u.Password, salt);
                u.Password = passwordHash;
                u.PasswordSalt = Convert.ToBase64String(salt);

                db.Users.Add(u);
            }
            else
            {
                var loginUser = db.Users.FirstOrDefault(e => e.Login.ToLower() == u.Login.ToLower());
                if (loginUser != null)
                {
                    if (loginUser.Id != u.Id)
                        throw new Exception($"Логин {u.Login} уже занят");
                }
                var emailUser = db.Users.FirstOrDefault(e => e.Email.ToLower() == u.Email.ToLower());
                if (emailUser != null)
                {
                    if (emailUser.Id != u.Id)
                    {
                        logger.Error($"Email {u.Email} уже занят");
                        throw new Exception($"Email {u.Email} уже занят");
                    }
                }

                if ((u.Password.Length < 6) && u.Password.Length > 0)
                {
                    logger.Error($"Длина пароля должна быть минимум 6 символов");
                    throw new Exception($"Длина пароля должна быть минимум 6 символов");
                }

                var salt = PasswordHelper.GetSecureSalt();
                var passwordHash = PasswordHelper.HashUsingPbkdf2(u.Password, salt);
                //u.Password = passwordHash;
                //u.PasswordSalt = Convert.ToBase64String(salt);

                user.Active = u.Active;
                user.Email = u.Email;
                user.Password = string.IsNullOrEmpty(u.Password) ? user.Password : passwordHash;
                user.PasswordSalt = string.IsNullOrEmpty(u.Password) ? user.PasswordSalt : Convert.ToBase64String(salt);
                user.Login = u.Login;
                user.Role = u.Role;
            }
            db.SaveChanges();
        }

        public void AddSpeciality(Specialty specialty)
        {
            if (db.Specialties.Any(e => e.Name.ToLower() == specialty.Name.ToLower()))
                throw new Exception($"В системе уже есть специальность {specialty.Name}");
            db.Specialties.Add(specialty);
            db.SaveChanges();
        }


        public IEnumerable<User> Fetch()
        {
            var res = db.Users.ToArray();
            return res;
        }

        public Specialty GetSpecialty(int id)
        {

            return db.Specialties.FirstOrDefault(e => e.Id == id);
        }

        public User GetUser(int userId)
        {
            var res = db.Users.FirstOrDefault(e => e.Id == userId);
            if (res == null)
                throw new Exception($"Пользователь с ID {userId} не найден");
            return res;
        }


        public IEnumerable<Doctor> Requests()
        {
            return db.Doctors.Where(e => !e.Confirm).ToArray();
        }
        public int ReqsCount()
        {
            return db.Doctors.Where(e => !e.Confirm).ToArray().Length;
        }

        public void ActivateRequest(int id, IConfiguration config)
        {//fZLoHVn3
            var req = db.Doctors.Include(e=>e.User).FirstOrDefault(e => e.Id == id);
            req.User.Password = Ext.RandomPassword();
            AddOrUpdateUser(req.User);
            req.Confirm = true;
            Email.Send(config, $"Ваша учетная запись подтверждена администратором.\r\nВаш пароль для входа в систему: {req.User.Password}", req.Email);
            db.SaveChanges();
        }

        public IEnumerable<Doctor> DocsBySpec(int specId) 
        {
            var docs = db.Doctors.Include(e => e.Specialties).Include(e => e.Schedulle)
                .Where(e => e.Specialties.Any(s => s.Id == specId)).Select(d=>new Doctor()
                {
                    Id = d.Id,
                    About = d.About,
                    Confirm= d.Confirm,
                    Lastname=d.Lastname,
                    Name=d.Name,
                    Photo=d.Photo,
                    StartWork=d.StartWork,
                    Email=d.Email,
                    Schedulle=d.Schedulle
                });
            return docs.ToArray();
        }


        public void RemoveUser(int userId)
        {
            logger.Trace($"Remove user id :{userId}");
            var user = db.Users.FirstOrDefault(e => e.Id == userId);
            if (user == null)
                throw new Exception($"Пользователь с ID {userId} не найден");

            db.Remove(user);
            db.SaveChanges();
        }

        public IEnumerable<Specialty> Specialties()
        {
            return db.Specialties.ToArray();
        }

        public Stream GetFiles(int id)
        {
            var req = db.Doctors.FirstOrDefault(e => e.Id == id);
            var root = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(root, $"{req.Email}");

            var zips = Path.Combine(root, $"zips");
            var zip = Path.Combine(zips, $"{req.Email}.zip");

            if (!Directory.Exists(zips))
                Directory.CreateDirectory(zips);



            if (File.Exists(zip))
            {
                File.Delete(zip);
            }
            ZipFile.CreateFromDirectory(path, zip);

            return File.OpenRead(zip);
        }


        #endregion

        #region Doctor
        public void CreateRequest(DoctorRequest req, IConfiguration config)
        {
            var root = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(root, $"{req.Email}");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (db.Users.Any(e => e.Email.ToLower() == req.Email.ToLower()))
                throw new Exception($"В системе уже есть пользователь с email {req.Email}");
            Doctor doctor = new Doctor()
            {
                About = req.About,
                Confirm = false,
                Email = req.Email,
                Lastname = req.Lastname,
                Name = req.Name,
                Middlename = req.Middlename,
                StartWork = req.StartWork,
                Photo = req.Photo,
                Schedulle = new Schedulle() { SchedulleJson = new List<SchedulleInstantiate>().ToJson() }
            };
            if (!String.IsNullOrEmpty(req.Summary))
            {
                var sumPath = Path.Combine(path, $"summary.pdf");
                File.WriteAllBytes(sumPath, Convert.FromBase64String(req.Summary));
                doctor.SummaryPath = sumPath;
            }
            if (!String.IsNullOrEmpty(req.EmpHistory))
            {
                var sumPath = Path.Combine(path, $"empHistory.pdf");
                File.WriteAllBytes(sumPath, Convert.FromBase64String(req.EmpHistory));
                doctor.EmpHistoryPath = sumPath;
            }
            if (!String.IsNullOrEmpty(req.Degree))
            {
                var sumPath = Path.Combine(path, $"degree.pdf");
                File.WriteAllBytes(sumPath, Convert.FromBase64String(req.Degree));
                doctor.DegreePath = sumPath;
            }

            doctor.Specialties = new List<Specialty>();

            foreach (var s in req.Specs)
            {
                doctor.Specialties.Add(db.Specialties.First(e => e.Id == s));
            }

            //Aj5xQ6G2 doctor@doctor.ru
            //TuMIRXZR doctor1@doctor.ru

            var password = Ext.RandomPassword();
            AddOrUpdateUser(new User()
            {
                Id = -1,
                Active = true,
                Email = req.Email,
                Login = req.Email,
                Password = password,
                PasswordSalt = String.Empty,
                Role = "doctor"
            });
            doctor.User = db.Users.First(e => e.Email.ToLower() == req.Email.ToLower());

            try
            {

                Email.Send(config, $"Ваша заявка принята.\r\n" +
                    $"Ваша учетная запись будет активирована после проверки администратором.");
            }
            catch (Exception ex)
            {
                db.Users.Remove(db.Users.First(e => e.Email.ToLower() == req.Email.ToLower()));
                db.SaveChanges();
                throw ex;
            }

            db.Doctors.Add(doctor);
            db.SaveChanges();

        }


        public bool GetConfirm(int id)
        {
            var doc = db.Doctors.Include(e => e.User).FirstOrDefault(e => e.User.Id == id);

            return doc.Confirm;
        }
        public Schedulle GetSchedulle(int id) 
        {
            var doc = db.Doctors.Include(e => e.User).Include(e=>e.Schedulle).FirstOrDefault(e => e.Id == id);
            return doc.Schedulle;
        }
        public void SaveSchedulle(SchedulleInstantiate[] req, int id) 
        {
            var doc = db.Doctors.Include(e => e.User).Include(e=>e.Schedulle).FirstOrDefault(e => e.Id == id);
            doc.Schedulle.SchedulleJson = req.ToJson();
            db.SaveChanges();
        }

        #endregion











    }
}
