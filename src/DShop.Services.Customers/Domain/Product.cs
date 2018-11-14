using System;
using DShop.Common.Types;

namespace DShop.Services.Customers.Domain
{
    public class Product : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        
        protected Product()
        {
        }

        public Product(Guid id, string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new DShopException("invalid_product_quantity",
                    "Product quantity cannot be negative.");
            }

            Quantity = quantity;
        }
    }
}