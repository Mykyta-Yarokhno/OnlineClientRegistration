using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClientRegistration.DataModels
{
    public class TimeTable
    {
        public int Id { get; set; }
        public string? NonWorkingDays { get; set; }
        public List<DateTime>? Holidays { get; set; }
        public List<DateTime>? DayOff { get; set; }
        public long StartWorkingTime { get; set; }
        public CustomTime CustomWorkingTime { get; set; }
        public int WorkingHours { get; set; } 
        public CustomTime CustomWorkingHours { get; set; }
        public long LunchStartTime { get; set; }
        public long LunchPeriod { get; set; }
        public CustomTime CustomLunchPeriod { get; set; }
    }
}
