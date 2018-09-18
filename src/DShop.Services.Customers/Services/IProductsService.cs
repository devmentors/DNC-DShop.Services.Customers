using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Dto;
using RestEase;

namespace DShop.Services.Customers.Services
{
    public interface IProductsService
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<ProductDto> GetAsync([Path] Guid id);
    }
}