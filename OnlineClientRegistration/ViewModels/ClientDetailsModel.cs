using Microsoft.AspNetCore.Mvc;

namespace OnlineClientRegistration.ViewModels
{
    public class ClientDetailsModel
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
