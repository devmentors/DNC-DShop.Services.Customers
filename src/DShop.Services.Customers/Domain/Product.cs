using System;
using DShop.Common.Types;

namespace DShop.Services.Customers.Domain
{
    public class Product : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        
        protected Product()
        {
        }

        public Product(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}