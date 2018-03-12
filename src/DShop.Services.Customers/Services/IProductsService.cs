using System;
using System.Threading.Tasks;

namespace DShop.Services.Customers.Services
{
    public interface IProductsService
    {
        Task CreateAsync(Guid id, string name, decimal price);
        Task UpdateAsync(Guid id, string name, decimal price);
        Task DeleteAsync(Guid id);
    }
}