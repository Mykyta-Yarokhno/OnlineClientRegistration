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
            return _context.Clients.FirstOrDefault(client => client.PhoneNumber == phoneNumber);
        }

        public bool ValidateUser(string phoneNumber)
        {
            return FindUser(phoneNumber) is not null;
        }
    }
}
