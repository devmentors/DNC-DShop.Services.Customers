using System.Linq;
using System.Threading.Tasks;
using DShop.Common.Handlers;
using DShop.Common.Types;
using DShop.Services.Customers.Dto;
using DShop.Services.Customers.Queries;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Handlers.Customers
{
    public class BrowseCustomersHandler : IQueryHandler<BrowseCustomers, PagedResult<CustomerDto>>
    {
        private readonly ICustomersRepository _customersRepository;

        public BrowseCustomersHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<PagedResult<CustomerDto>> HandleAsync(BrowseCustomers query)
        {
            var pagedResult = await _customersRepository.BrowseAsync(query);
            var customers = pagedResult.Items.Select(c => new CustomerDto
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Country = c.Country,
                CreatedAt = c.CreatedAt,
                Completed = c.Completed
            });

            return PagedResult<CustomerDto>.From(pagedResult, customers);
        }
    }
}