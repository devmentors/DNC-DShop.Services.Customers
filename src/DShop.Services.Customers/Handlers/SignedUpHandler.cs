using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Events.Customers;
using DShop.Messages.Events.Identity;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly ICustomerService _customerService;

        public SignedUpHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task HandleAsync(SignedUp @event, ICorrelationContext context)
        {
            await _customerService.AddAsync(@event.UserId, @event.Email);
        }
    }
}