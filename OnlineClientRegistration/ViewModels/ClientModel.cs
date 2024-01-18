using System.Runtime.Serialization;

namespace OnlineClientRegistration.ViewModels
{
    [DataContract]
    public class ClientModel
    {
        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
