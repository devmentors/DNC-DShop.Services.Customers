using System;

namespace DShop.Services.Customers.Domain
{
    public class Customer
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Address { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Customer()
        {
        }

        public Customer(Guid id, string email, string firstName,
            string lastName, string address)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            CreatedAt = DateTime.UtcNow;
        }
    }
}