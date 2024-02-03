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
                var role = GetUserRoleByMobilePhone(phoneNumber);

                var claims = new List<Claim>
                {
                    new(ClaimTypes.MobilePhone, phoneNumber),
                    new(ClaimTypes.Name, name),
                    new(ClaimTypes.Role, role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    // Set properties as needed
                };
                
                await HttpContext.SignOutAsync();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToPage(GetUserStartPage(role));
            }
            return Page();
        }

        private string GetUserStartPage(string role)
        {

            switch (role) 
            {
                case AccessRoles.Admin:
                case AccessRoles.Manager:
                        return "/Testing/OverallPrint";
                case AccessRoles.User:
                        return "/Testing/Create";
                default:
                    return "/Testing/Login";
            }
        }

        private string GetUserRoleByMobilePhone(string mobilePhone)
        {
            return mobilePhone switch
            {
                "+381212121242" => AccessRoles.Admin,
                "+381212121299" => AccessRoles.Manager,
                _ => AccessRoles.User,
            };
        }
    }
}

