using Microsoft.AspNetCore.Mvc;

namespace DShop.Services.Customers.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("DShop Customers Service");
    }
}