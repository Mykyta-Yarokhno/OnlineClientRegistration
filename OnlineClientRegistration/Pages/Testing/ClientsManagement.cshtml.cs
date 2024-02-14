using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineClientRegistration.Common.Security;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.Services;
using OnlineClientRegistration.ViewModels;
using System.Data;

namespace OnlineClientRegistration.Pages.Testing
{
    [Authorize(Roles = AccessRoles.Manager)]
    public class ClientsManagementModel : PageModel
    {
        private readonly UserService _userService;
        public IEnumerable<Client>? Clients { get; private set; }

        public ClientsManagementModel(UserService userService)
        {
            _userService = userService;
            
        }
        public void OnGet()
        {
            Clients = _userService.GetClients();
        }

        public async Task<IActionResult> OnPostAsync(ClientDetailsModel clientDetails)
        {
            //_userService.ChangeInfo(phoneNumber, name, notes);
            Clients = _userService.GetClients();
            return Page();
        }

        public async Task<PartialViewResult> OnGetClientDetails(string id)
        {
            var client = _userService.FindUser(id);
            return Partial("_ClientDetailsPartial", client);
        }

    }
}
