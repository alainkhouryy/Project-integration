namespace Basket.Api.Model
{
    public class BasketItem
    {
        public int id { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public decimal unitPrice { get; set; }
        public int quantity { get; set; }
    }
}
