using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet("GetCustomerData")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetCustomerData()
        {
            return Ok("You got the customer data!");
        }

        [HttpGet("GetAdminData")]
        [Authorize(Roles = "Admin")]
        public string GetAdminData()
        {
            return "You got the admin data!";
        }
    }
}
