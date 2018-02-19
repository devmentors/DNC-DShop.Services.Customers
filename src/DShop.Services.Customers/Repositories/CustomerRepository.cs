using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DShop.Services.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoDatabase _database;
        
        public CustomerRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Customer> GetAsync(Guid id)
            => await Customers
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Customer customer)
            => await Customers.InsertOneAsync(customer);

        private IMongoCollection<Customer> Customers 
            => _database.GetCollection<Customer>("Customers");
    }
}