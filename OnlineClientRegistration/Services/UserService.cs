using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.DataModels;
using OnlineClientRegistration.ViewModels;

namespace OnlineClientRegistration.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string NormalizePhoneNumber(string phoneNumber)
        {
            var corectPhoneNumber = phoneNumber.Trim();
            if (corectPhoneNumber[0] != '+')
                corectPhoneNumber = '+' + corectPhoneNumber;

            return corectPhoneNumber;
        }
        public Client? FindUser(string phoneNumber)
        {
            return _context.Clients
                .Include(client => client.UserRole)
                .Include(client => client.Notes)
                .FirstOrDefault(client => client.PhoneNumber == NormalizePhoneNumber(phoneNumber));
        }
        public async Task<bool> IsClientBlocked(string phoneNumber)
        {
            return await _context.ClientBlackLists
                    .Include(bl => bl.Client)
                    .AnyAsync(bl => 
                        bl.UserPhoneNumber == NormalizePhoneNumber(phoneNumber) 
                        || (bl.Client != null 
                            && bl.Client.PhoneNumber == NormalizePhoneNumber(phoneNumber))
                        );
        }
        public async Task<bool> IsClientExist(string phoneNumber)
        {
            return await _context.Clients
                    .AnyAsync(client => client.PhoneNumber == NormalizePhoneNumber(phoneNumber));
        }

        public bool ValidateUser(string phoneNumber)
        {
            return FindUser(phoneNumber) is not null;
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients
                .AsNoTracking()
                .Include(client => client.Notes)
                .Include(client => client.UserRole)
                .Include(client => client.BlackList)
                .Where(client => client.UserRole == null && client.BlackList == null)
                .ToList();
        }

        public void ChangeInfo(ClientDetailsModel clientDetails)
        {
            var client = FindUser(clientDetails.PhoneNumber); 

            if (client != null)
            {
                if (client.Name != clientDetails.Name)
                {
                    client.Name = clientDetails.Name;
                }

                if (client.Notes != null)
                {
                    if (client.Notes.Note != clientDetails.Notes)
                    {
                        client.Notes.Note = clientDetails.Notes;
                    }
                }
                else
                {
                    client.Notes = new ClientNotes { Note = clientDetails.Notes };
                }

                _context.SaveChangesAsync();
            }
        }

        public async Task BlockUserAsync(ClientDetailsModel clientDetails)
        {
            if(string.IsNullOrEmpty(clientDetails.BlockedReason))
            {
                throw new ArgumentException("Причина: обов'язкове поле.");
            }

            var client = 
                 _context
                    .Clients
                          .Include(client => client.BlackList)
                          .FirstOrDefault(client => client.PhoneNumber == clientDetails.PhoneNumber);

            if (client == null)
            {
                if (await _context.ClientBlackLists.AnyAsync(c => c.UserPhoneNumber == clientDetails.PhoneNumber))
                {
                    throw new Exception("Користувач з таким номером телефону вже заблокований.");
                }
            }
            else
            {
                if(client.BlackList != null)
                {
                    throw new Exception("Користувач з таким номером телефону вже заблокований.");
                }   
            }

            var clientBlackList = new ClientBlackList
            {
                ClientId = client?.Id,
                UserPhoneNumber = client == null ? clientDetails.PhoneNumber : null,
                Name = client == null ? clientDetails.Name : null,
                Reason = clientDetails.BlockedReason
            };

            _context.ClientBlackLists.Add(clientBlackList);

            await _context.SaveChangesAsync();   
            
        }
    }
    
}
