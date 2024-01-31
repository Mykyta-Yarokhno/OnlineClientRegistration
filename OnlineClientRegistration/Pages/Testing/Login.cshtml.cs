using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.Services;
using System.Security.Claims;
using OnlineClientRegistration.Common.Security;

namespace OnlineClientRegistration.Pages.Testing
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;

        public LoginModel(UserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string phoneNumber, string name)
        {
            var user = _userService.FindUser(phoneNumber);

            if(user == null)
            {
                _context.Clients.Add(new Client { PhoneNumber = phoneNumber, Name = name });

                await _context.SaveChangesAsync();
            }

            if (_userService.ValidateUser(phoneNumber))
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.MobilePhone, phoneNumber),
                    new(ClaimTypes.Name, name)
                };

                switch (phoneNumber)
                {
                    case "+381212121242":
                        claims.Add(new(ClaimTypes.Role, AccessRoles.Admin));
                        break;
                    case "+381212121299":
                        claims.Add(new(ClaimTypes.Role, AccessRoles.Manager));
                        break;
                    default:
                        claims.Add(new(ClaimTypes.Role, AccessRoles.User));
                        break;
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Set properties as needed
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToPage("/Testing/Create");
            }
            return Page();
        }
    }
}

