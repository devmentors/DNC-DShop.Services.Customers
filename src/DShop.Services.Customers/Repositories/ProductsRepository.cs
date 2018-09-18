using System;
using System.Threading.Tasks;
using DShop.Common.Mongo;
using DShop.Services.Customers.Domain;

namespace DShop.Services.Customers.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IMongoRepository<Product> _repository;
        
        public ProductsRepository(IMongoRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(Product product)
            => await _repository.AddAsync(product);

        public async Task UpdateAsync(Product product)
            => await _repository.UpdateAsync(product);

        public async Task DeleteAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}