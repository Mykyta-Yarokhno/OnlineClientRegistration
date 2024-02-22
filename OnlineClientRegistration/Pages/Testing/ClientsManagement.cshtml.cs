using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.Common.Security;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.Services;
using OnlineClientRegistration.ViewModels;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace OnlineClientRegistration.Pages.Testing
{
    [Authorize(Roles = AccessRoles.Manager)]
    public class ClientsManagementModel : PageModel
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;
        public IEnumerable<Client>? Clients { get; private set; }

        public ClientsManagementModel(UserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }
        public void OnGet()
        {
            Clients = _userService.GetClients();
        }

        public async Task<IActionResult> OnPostChangeFormAsync(ClientDetailsModel clientDetails)
        {
            _userService.ChangeInfo(clientDetails);
            Clients = _userService.GetClients();
            return Page();
        }

        public async Task<IActionResult> OnPostBlockFormAsync(ClientDetailsModel clientDetails)
        {
            try
            {
                await _userService.BlockUserAsync(clientDetails);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            Clients = _userService.GetClients();
            return Page();
        }

        public async Task<IActionResult> OnPostAddFormAsync(ClientDetailsModel clientDetails)
        {
            var isClientExist = await _userService.IsClientExist(clientDetails.PhoneNumber);

            var phoneNumberExistsInBlackList = await _context.ClientBlackLists
                                                .AnyAsync(c => c.UserPhoneNumber == clientDetails.PhoneNumber);

            if (!isClientExist && !phoneNumberExistsInBlackList)
            {
                _context.Clients.Add(new Client
                {
                    PhoneNumber = clientDetails.PhoneNumber,
                    Name = clientDetails.Name,
                    Notes = new ClientNotes { Note = clientDetails.Notes }
                });

                await _context.SaveChangesAsync();
            }
            else if(phoneNumberExistsInBlackList)
            {
                TempData["ErrorMessage"] = "Користувач з таким номером телефону у чорному списку.";
            }
            else
            {
                TempData["ErrorMessage"] = "Коритувач з таким номером телефону вже існує.";

            }

            Clients = _userService.GetClients();
            return Page();
        }


        public async Task<PartialViewResult> OnGetClientDetails(string id)
        {
            var client = _userService.FindUser(id);
            return Partial("_ClientDetailsPartial", client);
        }

        public async Task<PartialViewResult> OnGetAddClient()
        {
            return Partial("_AddClientPartial");
        }

        public async Task<PartialViewResult> OnGetBlockClient(string id)
        {
            var client = _userService.FindUser(id);
            return Partial("_BlockClientPartial", client);
        }

    }
}
