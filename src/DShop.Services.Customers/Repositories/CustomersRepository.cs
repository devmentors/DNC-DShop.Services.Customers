using System;
using System.Threading.Tasks;
using DShop.Common.Mongo;
using DShop.Services.Customers.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DShop.Services.Customers.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IMongoRepository<Customer> _repository;
        
        public CustomersRepository(IMongoRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(Customer customer)
            => await _repository.CreateAsync(customer);

        public async Task UpdateAsync(Customer customer)
            => await _repository.UpdateAsync(customer);
    }
}