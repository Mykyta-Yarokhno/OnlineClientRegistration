using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.Services;
using System.Security.Claims;
using OnlineClientRegistration.Common.Security;
using Microsoft.EntityFrameworkCore;

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

            if( await _userService.IsClientBlocked(phoneNumber))
            {
                return RedirectToPage("/Testing/Oops");
            }

            var user = _userService.FindUser(phoneNumber);

            if(user == null)
            {
                user = new Client { PhoneNumber = phoneNumber, Name = name };

                _context.Clients.Add(user);

                await _context.SaveChangesAsync();
            }

            var role = user.UserRole?.Role ?? AccessRoles.User;
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

        private string GetUserStartPage(string role)
        {

            switch (role) 
            {
                case AccessRoles.Admin:
                case AccessRoles.Manager:
                        return "/Testing/ClientsManagement";
                case AccessRoles.User:
                        return "/Testing/Create";
                default:
                    return "/Testing/Login";
            }
        }
    }
}

