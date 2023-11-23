using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class Client
    {
        [Key]
        public string PhoneNumber { get; set; }
        public string Name { get; set; }


    }
}
