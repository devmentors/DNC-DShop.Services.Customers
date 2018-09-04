using System;
using DShop.Common.Messages;
using Newtonsoft.Json;

namespace DShop.Services.Customers.Messages.Events
{
    [MessageNamespace("products")]
    public class ProductCreated : IEvent
    {
        public Guid Id { get; }

        [JsonConstructor]
        public ProductCreated(Guid id)
        {
            Id = id;
        }
    }
}
