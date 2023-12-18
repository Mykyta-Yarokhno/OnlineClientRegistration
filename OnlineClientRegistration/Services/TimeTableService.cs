using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.DataModels;

namespace OnlineClientRegistration.Services
{
    public class TimeTableService
    {
        private readonly ApplicationDbContext _context;

        public TimeTableService(ApplicationDbContext context)
        {
            _context = context;
        }
        public string[] GetNonworkingDays(List<int> selectedServices)
        {
            var dateListFromDatabase = _context.CustomDates.Where(item => item.IsWorkingDay == false).Select(item => item.Date).ToList();
            List<string> stringList = dateListFromDatabase.Select(date => date.ToString()).ToList();

            return stringList.ToArray();
        }

        public string[] GetWorkingDays()
        {
            var dateListFromDatabase = _context.CustomDates.Where(item => item.IsWorkingDay == true).Select(item => item.Date).ToList();
            List<string> stringList = dateListFromDatabase.Select(date => date.ToString()).ToList();

            return stringList.ToArray();
        }

        public string GetDaysOfWeekDisabled()
        {
            return _context.TimeTables.FirstOrDefault().NonWorkingDays;
        }
    }
}
