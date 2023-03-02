using PetHelper.Models.Buisness;
using WebKeyGenerator.Context;
using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Identity;
using WebKeyGenerator.Models.Requests;

namespace WebKeyGenerator.Services
{
    public interface IDataService
    {

       
        #region admin
        IEnumerable<User> Fetch();
        void AddOrUpdateUser(User user);
        void RemoveUser(int userId);
        User GetUser(int userId);


        void AddSpeciality(Specialty specialty);
        Specialty GetSpecialty(int id);

        IEnumerable<Specialty> Specialties();


        IEnumerable<Doctor> Requests();
        int ReqsCount();

        void ActivateRequest(int id, IConfiguration config);

        Stream GetFiles(int id);

        #endregion


        #region Doctor
        void CreateRequest(DoctorRequest req, IConfiguration config);

        bool GetConfirm(int id);

        Schedulle GetSchedulle(int id);
        void SaveSchedulle(SchedulleInstantiate[] req,int id);
        #endregion







    }
}
