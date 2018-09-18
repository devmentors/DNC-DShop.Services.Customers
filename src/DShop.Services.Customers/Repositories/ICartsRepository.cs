using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DShop.Services.Customers.Domain;

namespace DShop.Services.Customers.Repositories
{
    public interface ICartsRepository
    {
        Task<Cart> GetAsync(Guid id);
        Task<IEnumerable<Cart>> GetAllWithProduct(Guid productId);
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task UpdateManyAsync(IEnumerable<Cart> carts);
    }
}