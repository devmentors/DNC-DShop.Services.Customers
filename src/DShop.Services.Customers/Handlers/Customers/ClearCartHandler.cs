using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Customers;
using DShop.Messages.Events.Customers;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers.Customers
{
    public class ClearCartHandler : ICommandHandler<ClearCart>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IHandler _handler;
        private readonly ICartService _cartService;

        public ClearCartHandler(IBusPublisher busPublisher, 
            IHandler handler, ICartService cartService)
        {
            _busPublisher = busPublisher;
            _handler = handler;
            _cartService = cartService;
        }

        public async Task HandleAsync(ClearCart command, ICorrelationContext context)
            => await _handler.Handle(async () => 
                {
                    await _cartService.ClearAsync(command.CustomerId);
                    await _busPublisher.PublishEventAsync(new CartCleared(command.CustomerId), context);
                })
                .OnDShopError(async ex => await _busPublisher.PublishEventAsync(
                        new ClearCartRejected(command.CustomerId, ex.Message, ex.Code), context)
                )    
                .OnError(async ex => await _busPublisher.PublishEventAsync(
                        new ClearCartRejected(command.CustomerId, ex.Message, string.Empty), context)
                )
                .ExecuteAsync();
    }
}