using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Customers.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Ok("DShop Customers Service");
    }
}