using Microsoft.EntityFrameworkCore;
using OnlineClientRegistration.DataModels;

namespace OnlineClientRegistration.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Client? FindUser(string phoneNumber)
        {
            var corectPhoneNumber = phoneNumber.Trim();
            if (corectPhoneNumber[0] != '+')
                corectPhoneNumber = '+' + corectPhoneNumber;

            return _context.Clients
                .Include(client => client.UserRole)
                .Include(client => client.Notes)
                .FirstOrDefault(client => client.PhoneNumber == corectPhoneNumber);
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
                .Where(client => client.UserRole.Role == null)
                .ToList();
        }

        public void ChangeInfo(string phoneNumber, string name, string notes)
        {
            var client = FindUser(phoneNumber); 

            if (client != null)
            {
                if (client.Name != name)
                {
                    client.Name = name;
                }

                if (client.Notes != null)
                {
                    if (client.Notes.Note != notes)
                    {
                        client.Notes.Note = notes;
                    }
                }
                else
                {
                    client.Notes = new ClientNotes { UserPhoneNumber = client.PhoneNumber, Note = notes };
                }

                _context.SaveChangesAsync();
            }
        }
    }
    
}
