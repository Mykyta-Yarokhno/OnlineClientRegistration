using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class Client
    {
        [Key]
        public required string PhoneNumber { get; set; }
        public  required string Name { get; set; } 

    }
}
