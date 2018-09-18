using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Products
{
    public class ProductDeletedHandler : IEventHandler<ProductDeleted>
    {
        private readonly IHandler _handler;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public ProductDeletedHandler(IHandler handler, 
            ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _handler = handler;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(ProductDeleted @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                await _productsRepository.DeleteAsync(@event.Id);
                var carts = await _cartsRepository.GetAllWithProduct(@event.Id);
                foreach (var cart in carts)
                {
                    cart.DeleteProduct(@event.Id);
                }
                await _cartsRepository.UpdateManyAsync(carts);
            })
            .ExecuteAsync();
    }
}