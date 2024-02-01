using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using OnlineClientRegistration.DataModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using OnlineClientRegistration.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;
using OnlineClientRegistration.Common.Security;
using System.Xml.Linq;


namespace OnlineClientRegistration.Pages.Testing
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;

        public CreateModel(ApplicationDbContext db, UserService userService)
        {
            _context = db;
            _userService = userService;
        }

        [BindProperty]
        public Record NewRecord { get; set; }
        [BindProperty]
        public DateOnly DateSelected { get; set; }
        [BindProperty]
        public TimeOnly TimeSelected { get; set; }
        public SelectList ServiceSelectList { get; set; }
        public SelectList ChoosenServices { get; set; }
        public SelectList AvailableTimeRanges { get; set; }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var serviceTypes = await _context.ServiceTypes.ToListAsync();
            ServiceSelectList = new SelectList(serviceTypes, nameof(ServiceType.Id), nameof(ServiceType.Name));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if((User.Identity?.IsAuthenticated) != true)
            {
                return RedirectToPage("/Testing/Login");
            }

            var selectedServiceTypeIds = Request.Form["NewRecord.ServicesRequested"];

            var selectedServiceTypes = _context.ServiceTypes
                .Where(st => selectedServiceTypeIds.Contains(st.Id.ToString()))
                .ToList();


            if(User.IsInRole(AccessRoles.Manager))
            {
                var user = _userService.FindUser(NewRecord.ClientInfo.PhoneNumber);

                if (user == null)
                {
                    user = new Client
                    {
                        PhoneNumber = NewRecord.ClientInfo.PhoneNumber,
                        Name = NewRecord.ClientInfo.Name
                    };

                    _context.Clients.Add(user);

                    await _context.SaveChangesAsync();
                }

                NewRecord.ClientInfo = user;
            }
            else
            {
                NewRecord.ClientInfo = _userService.FindUser(User.FindFirst(System.Security.Claims.ClaimTypes.MobilePhone).Value);
                ModelState.ClearValidationState($"{nameof(NewRecord)}.{nameof(NewRecord.ClientInfo)}");
                ModelState.MarkFieldValid($"{nameof(NewRecord)}.{nameof(NewRecord.ClientInfo)}");
            }
            NewRecord.ServicesRequested = selectedServiceTypes;
            NewRecord.DateAndTime = new DateTime(DateSelected, TimeSelected);

           

            ChoosenServices = new SelectList(selectedServiceTypes, nameof(ServiceType.Id), nameof(ServiceType.Name));


            if (ModelState.IsValid)
            {
                if (DateSelected < DateOnly.FromDateTime(DateTime.Now) ||
                    DateSelected > DateOnly.FromDateTime(DateTime.Now.AddMonths(1)))
                {
                    var serviceTypes = await _context.ServiceTypes.ToListAsync();
                    ServiceSelectList = new SelectList(serviceTypes, nameof(ServiceType.Id), nameof(ServiceType.Name));
                    return Page();
                }

                if (TimeSelected == TimeOnly.FromDateTime(DateTime.MinValue))
                {
                    var serviceTypes = await _context.ServiceTypes.ToListAsync();
                    ServiceSelectList = new SelectList(serviceTypes, nameof(ServiceType.Id), nameof(ServiceType.Name));
                    return Page();
                }

            }   
            else
            {
                var serviceTypes = await _context.ServiceTypes.ToListAsync();
                ServiceSelectList = new SelectList(serviceTypes, nameof(ServiceType.Id), nameof(ServiceType.Name));
                return Page();
            }

            //var selectedServiceTypeIds = Request.Form["NewRecord.ServicesRequested"];

            //var selectedServiceTypes = _context.ServiceTypes
            //    .Where(st => selectedServiceTypeIds.Contains(st.Id.ToString()))
            //    .ToList();

            //NewRecord.ServicesRequested = selectedServiceTypes;

            _context.Records.Add(NewRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Testing/ClientPrint");
        }
    }
}
