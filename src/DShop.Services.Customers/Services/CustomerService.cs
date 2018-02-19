using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Domain;
using DShop.Services.Customers.Dtos;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(id);

            return customer == null ? null : new CustomerDto
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                CreatedAt = customer.CreatedAt
            };
        }

        public async Task AddAsync(Guid id, string email)
        {
            var customer = new Customer(id, email, null, null, null);
            await _customerRepository.AddAsync(customer);
        }
    }
}