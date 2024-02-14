using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class ClientNotes
    {
        [Key]
        public string UserPhoneNumber { get; set; }

        [ForeignKey("UserPhoneNumber")]
        public Client Client { get; set; }
        public  string? Note { get; set; }
    }
}
