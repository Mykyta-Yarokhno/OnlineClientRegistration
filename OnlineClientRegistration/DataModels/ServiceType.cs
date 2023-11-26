using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.DataModels
{
    public class ServiceType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public long TimeRequired { get; set; }
        public int Price { get; set; }
        public List<Record> Records { get; set; }

    }
}
