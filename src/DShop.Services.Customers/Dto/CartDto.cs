using System;
using System.Collections.Generic;

namespace DShop.Services.Customers.Dto
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public IEnumerable<CartItemDto> Items { get; set; }
    }
}