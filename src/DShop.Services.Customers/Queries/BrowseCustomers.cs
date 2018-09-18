using DShop.Common.Types;
using DShop.Services.Customers.Dto;

namespace DShop.Services.Customers.Queries
{
    public class BrowseCustomers : PagedQueryBase, IQuery<PagedResult<CustomerDto>>
    {
    }
}