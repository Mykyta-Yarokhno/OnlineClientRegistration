using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class CustomTime
    {
        [Key]
        public int NameId { get; set; }
        public DateTime Date { get; set; }
        public long Time { get; set; }
    }
}
