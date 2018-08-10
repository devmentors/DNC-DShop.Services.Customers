using System;

namespace DShop.Services.Customers.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name  { get; set; }
        public decimal Price { get; set; }
    }
}