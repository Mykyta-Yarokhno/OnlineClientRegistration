using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.Common.Security;

namespace OnlineClientRegistration.Pages.Testing
{
    [Authorize(Roles = AccessRoles.User)]
    public class ClientPrintModel : PageModel
    {
        ApplicationDbContext context;

        public List<Record> Records { get; private set; } = new();

        public ClientPrintModel(ApplicationDbContext db)
        {
            context = db;
        }
        public void OnGet()
        {
            Records = context.Records
                .Where(user => user.ClientInfo.PhoneNumber == User.GetMobilePhone())
                .OrderBy(user => user.DateAndTime).AsNoTracking()
                .Include(record => record.ServicesRequested)
                .ToList();
        }
    }
}
