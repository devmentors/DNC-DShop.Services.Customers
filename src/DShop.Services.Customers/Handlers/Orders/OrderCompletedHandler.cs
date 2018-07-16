using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Orders;
using DShop.Services.Customers.Repositories;
using DShop.Services.Customers.ServiceForwarders;

namespace DShop.Services.Customers.Handlers.Products
{
    public class OrderCompletedHandler : IEventHandler<OrderCompleted>
    {
        private readonly IHandler _handler;
        private readonly ICartsRepository _cartsRepository;

        public OrderCompletedHandler(IHandler handler, 
            ICartsRepository cartsRepository)
        {
            _handler = handler;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(OrderCompleted @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
                {
                    var cart = await _cartsRepository.GetAsync(@event.CustomerId);
                    cart.Clear();
                    await _cartsRepository.UpdateAsync(cart);
                })
                .ExecuteAsync();
    }
}