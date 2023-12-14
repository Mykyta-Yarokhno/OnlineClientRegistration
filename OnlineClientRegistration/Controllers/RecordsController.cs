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
        private readonly ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context,TimeTableService timeTableService)
        {
            _timeTableService = timeTableService;
            _context = context;
        }

        [HttpGet("disabledDates")]
        public IActionResult GetDisabledDates([FromQuery] List<int> selectedServices)
        {
            string days = _context.TimeTables.FirstOrDefault().NonWorkingDays;
            string[] dates = new string[] { };

            var result = new { days,dates };
            return Ok(result);
        }
    }
}
