using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Orders;
using DShop.Services.Customers.ServiceForwarders;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers.Products
{
    public class OrderCompletedHandler : IEventHandler<OrderCompleted>
    {
        private readonly IHandler _handler;
        private readonly ICartService _cartService;
        private readonly IOrdersApi _ordersApi;

        public OrderCompletedHandler(IHandler handler, 
            ICartService cartService,
            IOrdersApi ordersApi)
        {
            _handler = handler;
            _cartService = cartService;
            _ordersApi = ordersApi;
        }

        public async Task HandleAsync(OrderCompleted @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                var order = await _ordersApi.GetAsync(@event.Id);
                await _cartService.ClearAsync(order.CustomerId);
            })
            .ExecuteAsync();
    }
}