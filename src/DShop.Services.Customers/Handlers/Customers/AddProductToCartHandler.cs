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
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public AddProductToCartHandler(IBusPublisher busPublisher,
            ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _busPublisher = busPublisher;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(AddProductToCart command, ICorrelationContext context)
        {
            if (command.Quantity <= 0)
            {
                throw new DShopException(Codes.InvalidQuantity,
                    $"Invalid quantity: '{command.Quantity}'.");
            }

            var product = await _productsRepository.GetAsync(command.ProductId);
            if (product == null)
            {
                throw new DShopException(Codes.ProductNotFound,
                    $"Product: '{command.ProductId}' was not found.");
            }

            if (product.Quantity < command.Quantity)
            {
                throw new DShopException(Codes.NotEnoughProductsInStock,
                    $"Not enough products in stock: '{command.ProductId}'.");
            }

            var cart = await _cartsRepository.GetAsync(command.CustomerId);
            cart.AddProduct(product, command.Quantity);
            await _cartsRepository.UpdateAsync(cart);
            await _busPublisher.PublishAsync(new ProductAddedToCart(command.CustomerId,
                command.ProductId, command.Quantity), context);
        }
    }
}