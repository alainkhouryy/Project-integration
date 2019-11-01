using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ordering.Api.Infrastructure;

namespace Ordering.Api.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int productId { get; private set; }
        public string productName { get; set; }
        public decimal unitPrice { get; set; }
        public int units { get; set; }

        protected OrderItem() { }

        public OrderItem(int productId, string productName, decimal unitPrice, string pictureUrl, int units = 1)
        {
            if (units <= 0)
            {
                throw new OrderingDomainException("Invalid number of units");
            }

            this.productId = productId;

            this.productName = productName;
            this.unitPrice = unitPrice;

            this.units = units;
        }


        public void addUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }

            this.units += units;
        }

        
    }
}
