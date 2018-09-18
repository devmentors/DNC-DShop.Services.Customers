using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Customers.Domain;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Products
{
    public class ProductCreatedHandler : IEventHandler<ProductCreated>
    {
        private readonly IHandler _handler;
        private readonly IProductsRepository _productsRepository;

        public ProductCreatedHandler(IHandler handler, 
            IProductsRepository productsRepository)
        {
            _handler = handler;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(ProductCreated @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
            {
                var product = new Product(@event.Id, @event.Name, @event.Price);
                await _productsRepository.AddAsync(product);
            })
            .ExecuteAsync();
    }
}