using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class TimeTable
    {
        public string? NonWorkingDays { get; set; }
        public List<DateTime> Holidays { get; set; }
        public List<DateTime> DayOff { get; set; }
        public long StartWorkingTime { get; set; }
        public  List<(DateTime date, long time)>? CustomWorkingTime { get; set; }
        public int WorkingHours{ get; set; }
        public  List<(DateTime date, long hours)>? CustomWorkingHours { get; set; }
        public (long startTime,long endTime) LunchPeriod { get; set; }
        public  List<(DateTime date, long hours)>? CustomLunchPeriod { get; set; }
    }
}
