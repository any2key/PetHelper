using System.ComponentModel.DataAnnotations;

namespace PetHelper.Models.Buisness
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
