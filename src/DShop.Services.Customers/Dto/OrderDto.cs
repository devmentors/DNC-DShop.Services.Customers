using System;

namespace DShop.Services.Customers.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set ;}
        public Guid CustomerId { get; set; }
    }
}