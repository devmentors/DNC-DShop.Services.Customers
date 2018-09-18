using System;
using System.Threading.Tasks;
using DShop.Common.Types;
using DShop.Services.Customers.Domain;
using DShop.Services.Customers.Queries;

namespace DShop.Services.Customers.Repositories
{
    public interface ICustomersRepository
    {
        Task<Customer> GetAsync(Guid id);
        Task<PagedResult<Customer>> BrowseAsync(BrowseCustomers id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}