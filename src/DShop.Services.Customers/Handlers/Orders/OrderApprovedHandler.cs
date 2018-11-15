using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Orders
{
    public class OrderApprovedHandler : IEventHandler<OrderApproved>
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public OrderApprovedHandler(ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(OrderApproved @event, ICorrelationContext context)
        {
            var cart = await _cartsRepository.GetAsync(@event.CustomerId);
            foreach (var cartItem in cart.Items)
            {
                var product = await _productsRepository.GetAsync(cartItem.ProductId);
                if (product == null)
                {
                    continue;
                }

                product.SetQuantity(product.Quantity - cartItem.Quantity);
                await _productsRepository.UpdateAsync(product);
            }
        }
    }
}