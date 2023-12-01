using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using OnlineClientRegistration.DataModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OnlineClientRegistration.Pages.Testing
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext db)
        {
            _context = db;
        }

        [BindProperty]
        public Record NewRecord { get; set; }
        public SelectList ServiceSelectList { get; set; }
        public SelectList ChoosenServices { get; set; }
        [BindProperty]
        public DateOnly Date { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var serviceTypes = await _context.ServiceTypes.ToListAsync();
            ServiceSelectList = new SelectList(serviceTypes, nameof(ServiceType.Id), nameof(ServiceType.Name));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                
            }
            else
            {
                var serviceTypes = await _context.ServiceTypes.ToListAsync();
                ServiceSelectList = new SelectList(serviceTypes, nameof(ServiceType.Id), nameof(ServiceType.Name));
                return Page();
            }

            var selectedServiceTypeIds = Request.Form["NewRecord.ServicesRequested"];

            var selectedServiceTypes = _context.ServiceTypes
                .Where(st => selectedServiceTypeIds.Contains(st.Id.ToString()))
                .ToList();

            NewRecord.ServicesRequested = selectedServiceTypes;

            _context.Records.Add(NewRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Testing/Print");
        }
    }
}
