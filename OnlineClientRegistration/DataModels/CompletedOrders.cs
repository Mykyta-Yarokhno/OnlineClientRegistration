namespace OnlineClientRegistration.DataModels
{
    public class CompletedOrders
    {
        public Client ClientId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }

        public required string CompletedServices { get; set; }
    }
}
