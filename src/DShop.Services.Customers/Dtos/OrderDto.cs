using System;

namespace DShop.Services.Customers.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set ;}
        public Guid UserId { get; set; }
    }
}