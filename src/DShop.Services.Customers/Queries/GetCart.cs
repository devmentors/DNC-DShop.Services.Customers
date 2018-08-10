using System;
using DShop.Common.Types;
using DShop.Services.Customers.Dto;

namespace DShop.Services.Customers.Queries
{
    public class GetCart : IQuery<CartDto>
    {
        public Guid Id { get; set; }
    }
}