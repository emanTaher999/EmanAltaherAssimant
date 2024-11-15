using ASSIGNMENT.Models;
using ASSIGNMENT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASSIGNMENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public UserController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.RegisterAsync(model);
            if (!result.ISAuthentecated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authServices.LoginAsync(model);
            if (!result.ISAuthentecated)
                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}
