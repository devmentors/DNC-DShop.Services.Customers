using System;
using System.Threading.Tasks;
using DShop.Common.Dispatchers;
using DShop.Services.Customers.Dto;
using DShop.Services.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Customers.Controllers
{
    public class CartsController : BaseController
    {
        public CartsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> Get([FromRoute] GetCart query)
            => Single(await QueryAsync(query));
    }
}