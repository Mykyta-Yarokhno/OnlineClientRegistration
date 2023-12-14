using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClientRegistration.DataModels
{
    public class TimeTable
    {
        public int Id { get; set; }
        public string? NonWorkingDays { get; set; }
        public long StartWorkingTime { get; set; }
        public long EndWorkingTime { get; set; }
        //public long WorkingHours { get; set; }
    }
}
