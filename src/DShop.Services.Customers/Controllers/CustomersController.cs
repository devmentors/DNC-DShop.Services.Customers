using System;
using System.Threading.Tasks;
using DShop.Common.Dispatchers;
using DShop.Common.Types;
using DShop.Services.Customers.Dto;
using DShop.Services.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Customers.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CustomerDto>>> Get([FromQuery] BrowseCustomers query)
            => Collection(await QueryAsync(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
            => Single(await QueryAsync(query));
    }
}