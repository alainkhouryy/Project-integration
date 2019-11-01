using System;
using Microsoft.EntityFrameworkCore;
using Ordering.Api.Models;

namespace Ordering.Api.Data
{
    public class OrdersContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
        }
    }
}
