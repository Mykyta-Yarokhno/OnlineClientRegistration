using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class Client
    {
        [Key]
        public required string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Обов'язкове поле")]
        public  required string Name { get; set; } 

    }
}
