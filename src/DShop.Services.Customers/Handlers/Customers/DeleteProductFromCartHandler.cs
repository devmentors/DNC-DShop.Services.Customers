using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Customers.Messages.Commands;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Customers
{
    public class DeleteProductFromCartHandler : ICommandHandler<DeleteProductFromCart>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IHandler _handler;
        private readonly ICartsRepository _cartsRepository;
        private readonly IProductsRepository _productsRepository;

        public DeleteProductFromCartHandler(IBusPublisher busPublisher, 
            IHandler handler, ICartsRepository cartsRepository,
            IProductsRepository productsRepository)
        {
            _busPublisher = busPublisher;
            _handler = handler;
            _cartsRepository = cartsRepository;
            _productsRepository = productsRepository;
        }

        public async Task HandleAsync(DeleteProductFromCart command, ICorrelationContext context)
            => await _handler.Handle(async () => 
                {
                    var cart = await _cartsRepository.GetAsync(command.CustomerId);
                    cart.DeleteProduct(command.ProductId);
                    await _cartsRepository.UpdateAsync(cart);
                    await _busPublisher.PublishAsync(new ProductDeletedFromCart(command.CustomerId,
                        command.ProductId), context);
                })
                .OnCustomError(async ex => await _busPublisher.PublishAsync(
                        new DeleteProductFromCartRejected(command.CustomerId, command.ProductId,
                            ex.Message, ex.Code), context)
                )    
                .OnError(async ex => await _busPublisher.PublishAsync(
                        new DeleteProductFromCartRejected(command.CustomerId, command.ProductId,
                            ex.Message, string.Empty), context)
                )
                .ExecuteAsync();
    }
}