using ClothingWebAPI.Entities;
using ClothingWebAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothingWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public AuthenticationController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult authenticate([FromBody] KHACH_HANG_ENTITY user)
        {
            var token = jwtAuthenticationManager.authenticate(user.EMAIL, user.MAT_KHAU);
            if (token == null) { 
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
