using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineClientRegistration.DataModels;
using Microsoft.EntityFrameworkCore;

namespace OnlineClientRegistration.Pages.Testing
{
    public class PrintModel : PageModel
    {
        ApplicationDbContext context;

        public List<Record> Records { get; private set; } = new();

        public PrintModel(ApplicationDbContext db)
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
