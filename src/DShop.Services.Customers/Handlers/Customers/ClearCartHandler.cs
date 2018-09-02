using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Customers.Messages.Commands;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Customers
{
    public class ClearCartHandler : ICommandHandler<ClearCart>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IHandler _handler;
        private readonly ICartsRepository _cartsRepository;

        public ClearCartHandler(IBusPublisher busPublisher, 
            IHandler handler, ICartsRepository cartsRepository)
        {
            _busPublisher = busPublisher;
            _handler = handler;
            _cartsRepository = cartsRepository;
        }

        public async Task HandleAsync(ClearCart command, ICorrelationContext context)
            => await _handler.Handle(async () => 
                {
                    var cart = await _cartsRepository.GetAsync(command.CustomerId);
                    cart.Clear();
                    await _cartsRepository.UpdateAsync(cart);
                    await _busPublisher.PublishAsync(new CartCleared(command.CustomerId), context);
                })
                .OnCustomError(async ex => await _busPublisher.PublishAsync(
                        new ClearCartRejected(command.CustomerId, ex.Message, ex.Code), context)
                )    
                .OnError(async ex => await _busPublisher.PublishAsync(
                        new ClearCartRejected(command.CustomerId, ex.Message, string.Empty), context)
                )
                .ExecuteAsync();
    }
}