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
        private readonly IHandler _handler;
        private readonly ICustomersRepository _customersRepository;

        public SignedUpHandler(IHandler handler, 
            ICustomersRepository customersRepository)
        {
            _handler  = handler;
            _customersRepository = customersRepository;
        }

        public async Task HandleAsync(SignedUp @event, ICorrelationContext context)
            => await _handler.Handle(async () => 
                {
                    var customer = new Customer(@event.UserId, @event.Email);
                    await _customersRepository.CreateAsync(customer);
                }).ExecuteAsync();
    }
}