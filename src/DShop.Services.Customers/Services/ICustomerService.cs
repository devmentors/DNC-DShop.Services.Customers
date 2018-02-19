using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Dtos;

namespace DShop.Services.Customers.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetAsync(Guid id);
        Task AddAsync(Guid id, string email);
    }
}