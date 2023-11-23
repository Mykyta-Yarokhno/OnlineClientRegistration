using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class ServiceType
    {
        [Key]
        public string Name { get; set; }
        public long TimeRequired { get; set; }
        public int Price { get; set; }
    }
}
