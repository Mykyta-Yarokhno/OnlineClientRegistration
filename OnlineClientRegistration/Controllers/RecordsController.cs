using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineClientRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        [HttpGet("disabledDates")]
        public IActionResult GetDisabledDates()
        {
            string[] dates = { "14/12/2023", "15/12/2023" };

            return Ok(dates);
        }
    }
}
