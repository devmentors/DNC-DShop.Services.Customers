using System;

namespace DShop.Services.Customers.Dtos
{
    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        
    }
}