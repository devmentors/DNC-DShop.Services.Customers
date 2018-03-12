using System;
using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Messages.Commands.Customers;
using DShop.Messages.Events.Customers;
using DShop.Services.Customers.Services;

namespace DShop.Services.Customers.Handlers.Customers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICustomersService _customerService;
        private readonly IHandler _handler;

        public CreateCustomerHandler(IBusPublisher busPublisher,
            ICustomersService customerService,
            IHandler handler)
        {
            _busPublisher = busPublisher;
            _customerService = customerService;
            _handler = handler;
        }

        public async Task HandleAsync(CreateCustomer command, ICorrelationContext context)
            => await _handler.Handle(async () => 
                {
                    await _customerService.CompleteAsync(context.UserId, 
                        command.FirstName, command.LastName, command.Address, command.Country);
                    await _busPublisher.PublishEventAsync(new CustomerCreated(context.UserId),
                        context);
                })
                .OnDShopError(async ex => await _busPublisher.PublishEventAsync(
                        new CreateCustomerRejected(context.UserId, ex.Message, ex.Code), 
                            context)
                )    
                .OnError(async ex => await _busPublisher.PublishEventAsync(
                        new CreateCustomerRejected(context.UserId, ex.Message, string.Empty), 
                            context)
                )
                .ExecuteAsync();
    }
}