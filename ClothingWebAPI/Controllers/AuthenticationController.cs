using ClothingWebAPI.Entities;
using ClothingWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public AuthenticationController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult authenticate([FromBody] KHACH_HANG_ENTITY user)
        {
            var token = _jwtAuthenticationManager.authenticate(user.EMAIL, user.MAT_KHAU);
            if (token == null) { 
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
