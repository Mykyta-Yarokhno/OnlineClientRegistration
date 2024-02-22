using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.Common.Security;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.Services;
using OnlineClientRegistration.ViewModels;
using System.Text;

namespace OnlineClientRegistration.Pages.Testing
{
    [Authorize(Roles = AccessRoles.Manager)]
    public class BlackListModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService  _userService;

        public IEnumerable<ClientBlackList>? Clients { get; private set; }
        public BlackListModel(ApplicationDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public void OnGet()
        {
            Clients = _context.ClientBlackLists.AsNoTracking().ToList();
        }

        public async Task<PartialViewResult> OnGetAddClient()
        {
            return Partial("_AddClientToBlackListPartial");
        }

        public async Task<IActionResult> OnPostAddFormAsync(ClientDetailsModel clientDetails)
        {
            try
            {
                await _userService.BlockUserAsync(clientDetails);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = new StringBuilder(ex.Message)
#if DEBUG
                    .AppendLine(ex.InnerException?.ToString())
#endif
                    .ToString();
            }
           

            Clients = _context.ClientBlackLists.AsNoTracking().ToList();
            return Page();
        }
    }
}

