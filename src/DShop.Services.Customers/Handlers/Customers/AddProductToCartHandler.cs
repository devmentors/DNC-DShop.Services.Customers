using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Services.Customers.Messages.Commands;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Customers
{
    public class AddProductToCartHandler : ICommandHandler<AddProductToCart>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IHandler _handler;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public AddProductToCartHandler(IBusPublisher busPublisher, 
            IHandler handler, ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _busPublisher = busPublisher;
            _handler = handler;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(AddProductToCart command, ICorrelationContext context)
            => await _handler.Handle(async () => 
                {
                    var product = await _productsRepository.GetAsync(command.ProductId);
                    if (product == null)
                    {
                        throw new DShopException(Codes.ProductNotFound, 
                            $"Product: '{command.ProductId}' was not found.");
                    }
                    var cart = await _cartsRepository.GetAsync(command.CustomerId);
                    cart.AddProduct(product, command.Quantity);
                    await _cartsRepository.UpdateAsync(cart);
                    await _busPublisher.PublishAsync(new ProductAddedToCart(command.CustomerId,
                        command.ProductId, command.Quantity), context);
                })
                .OnDShopError(async ex => await _busPublisher.PublishAsync(
                        new AddProductToCartRejected(command.CustomerId, command.ProductId,
                            command.Quantity, ex.Message, ex.Code), context)
                )    
                .OnError(async ex => await _busPublisher.PublishAsync(
                        new AddProductToCartRejected(command.CustomerId, command.ProductId,
                            command.Quantity, ex.Message, string.Empty), context)
                )
                .ExecuteAsync();
    }
}