namespace Common.Messaging
{
    public class OrderCompletedEvent
    {
        public string buyerId { get; set; }
        public OrderCompletedEvent(string buyerId)
        {
            this.buyerId = buyerId;
        }
    }
}
