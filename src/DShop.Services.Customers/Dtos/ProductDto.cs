using System;

namespace DShop.Services.Customers.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name  { get; set; }
        public decimal Price { get; set; }
    }
}