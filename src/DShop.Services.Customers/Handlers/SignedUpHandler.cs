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
        private readonly IBusPublisher _busPublisher;
        private readonly ICustomerService _customerService;

        public SignedUpHandler(IBusPublisher busPublisher,
            ICustomerService customerService)
        {
            _busPublisher = busPublisher;
            _customerService = customerService;
        }

        public async Task HandleAsync(SignedUp @event, ICorrelationContext context)
        {
            await _customerService.AddAsync(@event.UserId, @event.Email, 
                @event.FirstName, @event.LastName, @event.Address);
            await _busPublisher.PublishEventAsync(new CustomerCreated(@event.RequestId, @event.UserId));
        }
    }
}