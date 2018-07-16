using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Products;
using DShop.Services.Customers.Domain;
using DShop.Services.Customers.Repositories;
using DShop.Services.Customers.ServiceForwarders;

namespace DShop.Services.Customers.Handlers.Products
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly IHandler _handler;
        private readonly IProductsApi _productsApi;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public ProductCreatedHandler(IHandler handler, 
            IProductsApi productsApi, 
            ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _handler = handler;
            _productsApi = productsApi;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(ProductCreated @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                var productDto = await _productsApi.GetAsync(@event.Id);
                var product = new Product(productDto.Id, productDto.Name, productDto.Price);
                await _productsRepository.CreateAsync(product);
            })
            .ExecuteAsync();
    }
}