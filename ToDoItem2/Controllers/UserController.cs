using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoItem2.Services;
using ToDoItem2.Models;

namespace ToDoItem2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUserData")]
        [Authorize(Policy = Policies.User)]
        [EnableQuery]
        public IActionResult GetUserData()
        {
            return Ok("This is a response from user method");
        }

        [HttpGet]
        [Route("GetAdminData")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAdminData()
        {
            return Ok("This is a response from Admin method");
        }
    }
}
