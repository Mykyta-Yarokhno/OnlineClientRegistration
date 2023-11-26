using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineClientRegistration.DataModels;
using System.Globalization;
using System.Net.WebSockets;

namespace OnlineClientRegistration.Pages.Testing
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        internal ApplicationDbContext context;
        [BindProperty]
        public DateTime DateAndTime { get; set; }
        [BindProperty]
        public string ServiceName { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        
        public CreateModel(ApplicationDbContext db)
        {
            context = db;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var serviceType = context.ServicesTypes.FirstOrDefault(service => service.Name == ServiceName);

            if (serviceType == null)
                return Page();

            var clientInfo = context.Clients.FirstOrDefault(phoneNumber => phoneNumber.PhoneNumber == PhoneNumber);

            if (clientInfo == null)
                return Page();


            var record = new Record() {
                DateAndTime = DateAndTime,
                ServicesRequested = [serviceType],
                ClientInfo = clientInfo
            };

            context.Records.Add(record);
            await context.SaveChangesAsync();
            return RedirectToPage("Print");
        }
    }
}
