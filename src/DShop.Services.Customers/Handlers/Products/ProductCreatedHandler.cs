using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Products;
using DShop.Services.Customers.ServiceForwarders;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers.Products
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly IHandler _handler;
        private readonly IProductsService _productsService;
        private readonly IProductsApi _productsApi;

        public ProductCreatedHandler(IHandler handler, 
            IProductsService productsService,
            IProductsApi productsApi)
        {
            _handler = handler;
            _productsService = productsService;
            _productsApi = productsApi;
        }

        public async Task HandleAsync(ProductCreated @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                var product = await _productsApi.GetAsync(@event.Id);
                await _productsService.CreateAsync(product.Id, product.Name, product.Price);
            })
            .ExecuteAsync();
    }
}