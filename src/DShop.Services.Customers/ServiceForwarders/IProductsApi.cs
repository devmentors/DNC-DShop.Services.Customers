using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Dtos;
using RestEase;

namespace DShop.Services.Customers.ServiceForwarders
{
    public interface IProductsApi
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<ProductDto> GetAsync([Path] Guid id);
    }
}