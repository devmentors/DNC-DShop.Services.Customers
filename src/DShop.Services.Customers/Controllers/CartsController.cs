using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Customers.Controllers
{
    [Route("[controller]")]
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _cartService.GetAsync(id));
    }
}