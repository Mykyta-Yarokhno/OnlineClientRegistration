using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClientRegistration.DataModels
{
   
    public class UserRole
    {
        [Key]
        public string UserPhoneNumber { get; set; } 

        [ForeignKey("UserPhoneNumber")]
        public Client Client { get; set; }
        public required string Role { get; set; }
    }
}