using OnlineClientRegistration.Common.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClientRegistration.DataModels
{
    public class Client
    {
        [Key]
        public required string PhoneNumber { get; set; }
        public  required string Name { get; set; }
        public UserRole? UserRole { get; set; }
        public ClientNotes? Notes { get; set; }

    }
}
