using System;
using DShop.Common.Types;
using DShop.Services.Customers.Dtos;

namespace DShop.Services.Customers.Queries
{
    public class GetCustomer : IQuery<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}