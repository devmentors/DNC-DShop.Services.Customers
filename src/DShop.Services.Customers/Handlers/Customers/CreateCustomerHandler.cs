using System;
using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Common.Types;
using DShop.Services.Customers.Messages.Commands;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Domain;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Customers
{
    public class CreateCustomerHandler : ICommandHandler<CreateCustomer>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ICartsRepository _cartsRepository;
        private readonly ICustomersRepository _customersRepository;
        private readonly IHandler _handler;

        public CreateCustomerHandler(IBusPublisher busPublisher,
            ICartsRepository cartsRepository,
            ICustomersRepository customersRepository,
            IHandler handler)
        {
            _busPublisher = busPublisher;
            _cartsRepository = cartsRepository;
            _customersRepository = customersRepository;
            _handler = handler;
        }

        public async Task HandleAsync(CreateCustomer command, ICorrelationContext context)
            => await _handler.Handle(async () => 
                {
                    var customer = await _customersRepository.GetAsync(command.Id);
                    if (customer.Completed)
                    {
                        throw new DShopException(Codes.CustomerAlreadyCompleted, 
                            $"Customer account was already completed for user: '{command.Id}'.");
                    }
                    customer.Complete(command.FirstName, command.LastName, 
                        command.Address, command.Country);
                    await _customersRepository.UpdateAsync(customer);
                    var cart = new Cart(command.Id);
                    await _cartsRepository.CreateAsync(cart);
                    await _busPublisher.PublishAsync(new CustomerCreated(command.Id), context);
                })
                .OnCustomError(async ex => await _busPublisher.PublishAsync(
                        new CreateCustomerRejected(command.Id, ex.Message, ex.Code), context)
                )    
                .OnError(async ex => await _busPublisher.PublishAsync(
                        new CreateCustomerRejected(command.Id, ex.Message, string.Empty), context)
                )
                .ExecuteAsync();
    }
}