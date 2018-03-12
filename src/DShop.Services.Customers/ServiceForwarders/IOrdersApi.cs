using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Dtos;
using RestEase;

namespace DShop.Services.Customers.ServiceForwarders
{
    public interface IOrdersApi
    {
        [AllowAnyStatusCode]
        [Get("orders/{id}")]
        Task<OrderDto> GetAsync([Path] Guid id);
    }
}