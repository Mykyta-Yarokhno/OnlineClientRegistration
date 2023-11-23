using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineClientRegistration.DataModels
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public required Client User { get; set; }
        public required List<ServiceType> ServicesRequested { get; set; }
    }
}
