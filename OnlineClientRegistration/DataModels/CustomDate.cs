using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class CustomDate
    {
        [Key]
        public DateOnly Date { get; set; }
        public bool IsWorkingDay { get; set; }
    }
}
