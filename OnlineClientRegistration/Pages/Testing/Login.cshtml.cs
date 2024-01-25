using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineClientRegistration.DataModels;

namespace OnlineClientRegistration.Pages.Testing
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;

        public LoginModel(UserManager<Client> userManager, SignInManager<Client> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public string PhoneNumber { get; set; }
        public void OnGet()
        {
        }
    }
}
