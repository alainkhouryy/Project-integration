using System.Collections.Generic;

namespace Basket.Api.Model
{
    public class Basket
    {
        public string customerId { get; set; }
        public List<BasketItem> items { get; set; }

        public Basket(string customerId)
        {
            this.customerId = customerId;
            this.items = new List<BasketItem>();
        }
    }
}
