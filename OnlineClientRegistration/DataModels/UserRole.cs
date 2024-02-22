using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClientRegistration.DataModels
{
   
    public class UserRole
    {
        [Key]
        public int ClientId { get; set; } 

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public required string Role { get; set; }
    }
}