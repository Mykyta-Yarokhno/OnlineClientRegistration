using Microsoft.AspNetCore.Mvc;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.Services;
using OnlineClientRegistration.ViewModels;

namespace OnlineClientRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserInfoService _userInfoService;

        public UsersController( UserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpGet("user")]
        public IActionResult GetUserInfo([FromQuery] string phoneNumber)
        {
            var user = _userInfoService.FindUser(phoneNumber);

            if(user != null)
                return Ok(new ClientModel() { PhoneNumber = user.PhoneNumber, Name = user.Name });

            return NoContent();
        }
    }
}
