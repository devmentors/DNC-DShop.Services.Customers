using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Customers;
using DShop.Messages.Events.Customers;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers.Customers
{
    public class DeleteProductFromCartHandler : ICommandHandler<DeleteProductFromCart>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IHandler _handler;
        private readonly ICartService _cartService;

        public DeleteProductFromCartHandler(IBusPublisher busPublisher, 
            IHandler handler, ICartService cartService)
        {
            _busPublisher = busPublisher;
            _handler = handler;
            _cartService = cartService;
        }

        public async Task HandleAsync(DeleteProductFromCart command, ICorrelationContext context)
            => await _handler.Handle(async () => 
                await _cartService.DeleteProductAsync(context.UserId, command.ProductId))
                .OnDShopError(async ex => await _busPublisher.PublishEventAsync(
                        new DeleteProductFromCartRejected(context.UserId, command.ProductId,
                            ex.Message, ex.Code), context)
                )    
                .OnError(async ex => await _busPublisher.PublishEventAsync(
                        new DeleteProductFromCartRejected(context.UserId, command.ProductId,
                            ex.Message, string.Empty), context)
                )
                .ExecuteAsync();
    }
}