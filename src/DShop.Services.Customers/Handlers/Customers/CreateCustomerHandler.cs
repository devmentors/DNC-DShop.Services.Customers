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

        public CreateCustomerHandler(IBusPublisher busPublisher,
            ICartsRepository cartsRepository,
            ICustomersRepository customersRepository)
        {
            _busPublisher = busPublisher;
            _cartsRepository = cartsRepository;
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(CreateCustomer command, ICorrelationContext context)
        {
            var customer = await _customersRepository.GetAsync(command.Id);
            if (customer.Completed)
            {
                throw new DShopException(Codes.CustomerAlreadyCompleted,
                    $"Customer account was already created for user with id: '{command.Id}'.");
            }

            customer.Complete(command.FirstName, command.LastName, command.Address, command.Country);
            await _customersRepository.UpdateAsync(customer);
            var cart = new Cart(command.Id);
            await _cartsRepository.AddAsync(cart);
            await _busPublisher.PublishAsync(new CustomerCreated(command.Id, customer.Email,
                command.FirstName, command.LastName, command.Address, command.Country), context);
        }
    }
}