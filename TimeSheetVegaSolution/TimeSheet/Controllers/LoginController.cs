using Contracts.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            UserModel user = _loginService.AuthenticateUser(login);
            return Ok(new { token = _loginService.GenerateJSONWebToken(user) });
        }
    }
}
