using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebKeyGenerator.Utils;

namespace PetHelper.Models.Buisness
{
    public class Schedulle
    {
        [Key]
        public int Id { get; set; }
        public string SchedulleJson { get; set; }

        [NotMapped]
        public List<SchedulleInstantiate> Schedulles
            => String.IsNullOrEmpty(SchedulleJson) ?
            new List<SchedulleInstantiate>() :
            SchedulleJson.FromJson<List<SchedulleInstantiate>>();
    }


    public class SchedulleInstantiate
    {
        public DayOfWeek Day { get; set; }
        public TimePeriod Begin { get; set; }
        public TimePeriod End { get; set; }
    }

    public class TimePeriod
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
    public enum DayOfWeek
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
    }
}
