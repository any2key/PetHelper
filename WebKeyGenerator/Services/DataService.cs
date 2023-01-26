using Microsoft.EntityFrameworkCore;
using PetHelper.Models.Buisness;
using System.Globalization;
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


        #endregion











    }
}
