using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Products;
using DShop.Services.Customers.Repositories;
using DShop.Services.Customers.ServiceForwarders;

namespace DShop.Services.Customers.Handlers.Products
{
    public class ProductDeletedHandler : IEventHandler<ProductDeleted>
    {
        private readonly IHandler _handler;
        private readonly IProductsApi _productsApi;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public ProductDeletedHandler(IHandler handler, 
            IProductsApi productsApi, 
            ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _handler = handler;
            _productsApi = productsApi;
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