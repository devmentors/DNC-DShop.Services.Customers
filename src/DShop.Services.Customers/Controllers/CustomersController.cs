using System;
using System.Threading.Tasks;
using DShop.Services.Customers.Services;
using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Customers.Controllers
{
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _customerService.GetAsync(id));
    }
}