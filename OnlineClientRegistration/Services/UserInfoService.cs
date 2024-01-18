using OnlineClientRegistration.DataModels;

namespace OnlineClientRegistration.Services
{
    public class UserInfoService
    {
        private readonly ApplicationDbContext _context;

        public UserInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Client? FindUser(string phoneNumber)
        {
            return _context.Clients.FirstOrDefault(client => client.PhoneNumber == phoneNumber);
        }
    }
}
