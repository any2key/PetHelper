using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebKeyGenerator.Models.Identity;

namespace PetHelper.Models.Buisness
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public int StartWork { get; set; }
        public string? Photo { get; set; }
        public string? SummaryPath { get; set; }
        public string? EmpHistoryPath { get; set; }
        public string? DegreePath { get; set; }
        public ICollection<Specialty> Specialties { get; set; }
        public string? About { get; set; }
        public string? Email { get; set; }
        public bool Confirm { get; set; }

        public User User { get; set; }
        public Schedulle? Schedulle { get; set; }

    }


    public class DoctorRequest
    {
        public string Photo { get; set; }
        public string Summary { get; set; }
        public string EmpHistory { get; set; }
        public string Degree { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public int StartWork { get; set; }

        public string About { get; set; }
        public string Email { get; set; }
        public List<int> Specs { get; set; }

    }

    public class FileInfoReq
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }
}
