using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.DataModels;
using System.Reflection.Metadata.Ecma335;

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

        public List<TimeOnly> GetAvailableTimes(DateOnly selectedDate)
        {
            var unavailable = _context.Records
                    .Where(item => DateOnly.FromDateTime(item.DateAndTime) == selectedDate)
                    .Select(item => TimeOnly.FromDateTime(item.DateAndTime))
                    .ToList();

            List<TimeOnly> times = GetDefaultTimes();

            return times.Where(item => !unavailable.Contains(item)).ToList();
               
        }

        private List<TimeOnly> GetDefaultTimes()
        {
            return new List<TimeOnly>()
            {
                new(10, 0),
                new(13, 0),
                new(16,0),
                new(19,0)
            };
        }

    }
}
