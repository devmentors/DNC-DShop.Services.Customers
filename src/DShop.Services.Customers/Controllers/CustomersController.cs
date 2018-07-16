using System;
using System.Threading.Tasks;
using DShop.Common.Dispatchers;
using DShop.Services.Customers.Dtos;
using DShop.Services.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Customers.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
            => Single(await DispatchAsync<GetCustomer, CustomerDto>(query));
    }
}