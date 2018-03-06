using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Messages.Commands.Customers;
using DShop.Messages.Events.Customers;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICustomersService _customerService;

        public CreateCustomerHandler(IBusPublisher busPublisher,
            ICustomersService customerService)
        {
            _busPublisher = busPublisher;
            _customerService = customerService;
        }

        public async Task HandleAsync(CreateCustomer command, ICorrelationContext context)
        {
            await _customerService.CompleteAsync(context.UserId, 
                command.FirstName, command.LastName, command.Address, command.Country);
            await _busPublisher.PublishEventAsync(new CustomerCreated(context.UserId));
        }        
    }
}