using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Customers.Messages.Events
{
    [MessageNamespace("products")]
    public class ProductUpdated : IEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }

        [JsonConstructor]
        public ProductUpdated(Guid id, string name, 
            string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
