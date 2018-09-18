using System;
using DShop.Common.Types;

namespace DShop.Services.Customers.Domain
{
    public class Customer : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public string Country { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Completed => CompletedAt.HasValue;
        public DateTime? CompletedAt { get; private set; }

        protected Customer()
        {
        }

        public Customer(Guid id, string email)
        {
            Id = id;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }

        public void Complete(string firstName, string lastName, 
            string address, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Country = country;
            CompletedAt = DateTime.UtcNow;
        }
    }
}