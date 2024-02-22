using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.Common.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClientRegistration.DataModels
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Client
    {
        [Key]
        public  int Id { get; set; }
        [MaxLength(13)]
        public required string PhoneNumber { get; set; }
        public  required string Name { get; set; }
        public virtual UserRole? UserRole { get; set; }
        public virtual ClientNotes? Notes { get; set; }
        public virtual  ClientBlackList? BlackList { get; set; }

    }
}
