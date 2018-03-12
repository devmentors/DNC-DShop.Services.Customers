using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Dtos;

namespace DShop.Services.Customers.Services
{
    public interface ICartService
    {
        Task<CartDto> GetAsync(Guid userId);
        Task AddProductAsync(Guid userId, Guid productId, int quantity = 1);
        Task DeleteProductAsync(Guid userId, Guid productId);
        Task ClearAsync(Guid userId);
        Task HandleUpdatedProductAsync(Guid productId); 
        Task HandleDeletedProductAsync(Guid productId);    
    }
}