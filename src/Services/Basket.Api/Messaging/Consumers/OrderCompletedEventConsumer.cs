using System;
using System.Threading.Tasks;
using Basket.Api.Model;
using MassTransit;
using Microsoft.Extensions.Logging;
using Common.Messaging;

namespace Basket.Api.Messaging.Consumers
{
    public class OrderCompletedEventConsumer : IConsumer<OrderCompletedEvent>
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<OrderCompletedEventConsumer> _logger;

        public OrderCompletedEventConsumer(IBasketRepository repository, ILogger<OrderCompletedEventConsumer> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            _logger.LogWarning("We are in consume method now...");
            _logger.LogWarning("BuyerId:" + context.Message.buyerId);
            return _repository.deleteBasketAsync(context.Message.buyerId);
        }
    }
}
