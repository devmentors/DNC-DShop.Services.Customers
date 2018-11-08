using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Orders
{
    public class OrderCompletedHandler : IEventHandler<OrderCompleted>
    {
        private readonly ICartsRepository _cartsRepository;

        public OrderCompletedHandler(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(OrderCompleted @event, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(@event.CustomerId);
            cart.Clear();
            await _cartsRepository.UpdateAsync(cart);
        }
    }
}