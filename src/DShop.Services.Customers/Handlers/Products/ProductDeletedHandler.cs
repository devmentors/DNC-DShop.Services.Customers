using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Products;
using DShop.Services.Customers.ServiceForwarders;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers.Products
{
    public class ProductDeletedHandler : IEventHandler<ProductDeleted>
    {
        private readonly IHandler _handler;
        private readonly IProductsService _productsService;
        private readonly IProductsApi _productsApi;
        private readonly ICartService _cartService;

        public ProductDeletedHandler(IHandler handler, 
            IProductsService productsService,
            ICartService cartService,
            IProductsApi productsApi)
        {
            _handler = handler;
            _productsService = productsService;
            _productsApi = productsApi;
            _cartService = cartService;
        }

        public async Task HandleAsync(ProductDeleted @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                var product = await _productsApi.GetAsync(@event.Id);
                await _productsService.UpdateAsync(product.Id, product.Name, product.Price);
                await _cartService.HandleDeletedProductAsync(product.Id);
            })
            .ExecuteAsync();
    }
}