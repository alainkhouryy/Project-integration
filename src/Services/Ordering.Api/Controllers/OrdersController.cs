using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Api.Data;
using Common.Messaging;
using Ordering.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ordering.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrdersContext _ordersContext;
        private readonly IOptionsSnapshot<OrderingSettings> _settings;

        private readonly ILogger<OrdersController> _logger;
        private IBus _bus;

        public OrdersController(OrdersContext ordersContext, ILogger<OrdersController> logger, IOptionsSnapshot<OrderingSettings> settings, IBus bus)
        {
            _ordersContext = ordersContext;
            _logger = logger;
            _settings = settings;
            _bus = bus;
            ((DbContext)ordersContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpPost("new")]
        public async Task<IActionResult> createOrder([FromBody] Order order)
        {
            var conString = _settings.Value.connectionString;
            _logger.LogInformation($"{conString}");

            order.orderDate = DateTime.UtcNow.ToString();

            _logger.LogInformation(" In Create Order");


            _ordersContext.Orders.Add(order);
            _ordersContext.orderItems.AddRange(order.orderItems);

            _logger.LogInformation(" Order added to context");
            _logger.LogInformation(" Saving........");
            try
            {
                await _ordersContext.SaveChangesAsync();
                _bus.Publish(new OrderCompletedEvent(order.buyerId)).Wait();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("An error occored during Order saving .." + ex.Message);
                return BadRequest();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> getOrder(int id)
        {
            var item = await _ordersContext.Orders
                .SingleOrDefaultAsync(ci => ci.orderId == id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();

        }

        [HttpGet("")]
        public async Task<IActionResult> getOrders()
        {
            var orders = _ordersContext.Orders.Include(x => x.orderItems);
            if (orders.Count() > 0)
            {
                var ordersList = await orders.ToListAsync();
                return Ok(ordersList);
            }
            return Ok(orders);
        }

    }
}
