using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClientRegistration.DataModels
{
    [Keyless]
    public class TimeTable
    {
        public string? NonWorkingDays { get; set; }
        public int StartWorkingTime { get; set; }
        public int WorkingHours { get; set; }
    }
}
