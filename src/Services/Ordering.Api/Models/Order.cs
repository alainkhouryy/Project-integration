using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Api.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; }
        public string buyerId { get; set; }
        public string orderDate { get; set; }
        public decimal orderTotal { get; set; }
        public IEnumerable<OrderItem> orderItems { get; set; }

        protected Order()
        {
            this.orderItems = new List<OrderItem>();
        }
    }
}
