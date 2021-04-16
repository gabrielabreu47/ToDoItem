using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoItem2.Services;
using ToDoItem2.BL.Dtos;

namespace ToDoItem2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;
        public LoginController(ILogger<LoginController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserDto login)
        {
            UserDto user = _userService.AuthenticateUser(login);
            if (user is null)
                return Unauthorized();

            var tokenString = _userService.GenerateJWTToken(user);
            return Ok(new
            {
                token = tokenString,
                userDetails = user,
            });
        }
       
       

    }
}
