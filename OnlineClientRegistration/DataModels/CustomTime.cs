using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class CustomTime
    {
        [Key]
        public DateTime Date { get; set; }
        public long StartTime { get; set; }
        public long Period { get; set; }
    }
}
