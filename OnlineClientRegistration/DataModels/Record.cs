using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Reflection.Metadata.BlobBuilder;

namespace OnlineClientRegistration.DataModels
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public required List<ServiceType> ServicesRequested { get; set; } 
        public required Client ClientInfo { get; set; }
    }
}
