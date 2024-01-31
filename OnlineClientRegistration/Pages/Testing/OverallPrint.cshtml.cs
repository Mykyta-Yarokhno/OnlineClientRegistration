using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineClientRegistration.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using OnlineClientRegistration.Common.Security;

namespace OnlineClientRegistration.Pages.Testing
{
    [Authorize(Roles =AccessRoles.Admin)]
    public class OverallPrintModel : PageModel
    {
        ApplicationDbContext context;

        public List<Record> Records { get; private set; } = new();

        public OverallPrintModel(ApplicationDbContext db)
        {
            context = db;
        }
        public void OnGet()
        {
            Records = context.Records.AsNoTracking()
                .Include(record => record.ServicesRequested)
                .Include(record => record.ClientInfo)
                .ToList();
        }
    }
}
