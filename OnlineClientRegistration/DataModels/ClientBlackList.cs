using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineClientRegistration.DataModels
{
    public class ClientBlackList
    {
        [Key]
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string? UserPhoneNumber { get; set; } 
        public string? Name { get; set; }
        public required string Reason { get; set; }
        [Column(TypeName = "datetime")]
        public  DateTime BlockedTime { get; set; } = DateTime.UtcNow;
        [ForeignKey("ClientId")]
        public virtual Client? Client { get; set; } 
    }
}
