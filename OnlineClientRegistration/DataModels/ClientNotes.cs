﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class ClientNotes
    {
        [Key]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public  string? Note { get; set; }
    }
}
