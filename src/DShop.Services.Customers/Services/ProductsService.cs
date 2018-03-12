using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Domain;
using DShop.Services.Customers.Repositories;

namespace DShop.Services.Customers.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task CreateAsync(Guid id, string name, decimal price)
            => await _productsRepository.CreateAsync(new Product(id, name, price));

        public async Task UpdateAsync(Guid id, string name, decimal price)
            => await _productsRepository.UpdateAsync(new Product(id, name, price));

        public async Task DeleteAsync(Guid id)
            => await _productsRepository.DeleteAsync(id);
    }
}