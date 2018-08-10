using System;
using DShop.Common.Types;
using DShop.Services.Customers.Dto;

namespace DShop.Services.Customers.Queries
{
    public class GetCustomer : IQuery<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}