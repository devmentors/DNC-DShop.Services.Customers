using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.RabbitMq;
using DShop.Services.Customers.Messages.Events;
using DShop.Services.Customers.Domain;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Identity
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly ICustomersRepository _customersRepository;

        public SignedUpHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(SignedUp @event, ICorrelationContext context)
        {
            var customer = new Customer(@event.UserId, @event.Email);
            await _customersRepository.AddAsync(customer);
        }
    }
}