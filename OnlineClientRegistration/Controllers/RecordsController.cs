using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.Services;

namespace OnlineClientRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly TimeTableService _timeTableService;

        public RecordsController(ApplicationDbContext context,TimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
        }

        [HttpGet("disabledDates")]
        public IActionResult GetDisabledDates([FromQuery] List<int> selectedServices)
        {
            string days = _timeTableService.GetDaysOfWeekDisabled();
            string[] dates = _timeTableService.GetNonworkingDays(selectedServices);
            string[] openDates = _timeTableService.GetWorkingDays();

            var result = new { days,dates,openDates };
            return Ok(result);
        }
    }
}
