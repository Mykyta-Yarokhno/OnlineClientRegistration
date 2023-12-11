using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineClientRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        [HttpGet]
        [Route("disabledDates")]
        public IActionResult GetDisabledDates()
        {
            string[] dates = ["11/12/2023", "12/12/2023"];

            return Ok(dates);
        }
    }
}
