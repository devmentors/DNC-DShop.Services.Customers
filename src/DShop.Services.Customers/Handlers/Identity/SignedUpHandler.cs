using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Customers;
using DShop.Messages.Events.Identity;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers.Identity
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly IHandler _handler;
        private readonly ICustomersService _customerService;

        public SignedUpHandler(IHandler handler, 
            ICustomersService customerService)
        {
            _handler = handler;
            _customerService = customerService;
        }

        public async Task HandleAsync(SignedUp @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
                await _customerService.CreateAsync(@event.UserId, @event.Email))
                .ExecuteAsync();

    }
}