using ClothingWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhachHangController : ControllerBase
    {
        private readonly ILogger<KhachHangController> _logger;
        private readonly JWTSettings _jwtsettings;
        private readonly IConfiguration _configuration;
        public KhachHangController(IConfiguration configuration, ILogger<KhachHangController> logger, IOptions<JWTSettings> jwtsettings)
        {
            _logger = logger;
            _configuration = configuration;
            _jwtsettings = jwtsettings.Value;
        }

        // POST: api/KhachHang
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<KHACH_HANG_w_TOKEN>> Login(KHACH_HANG_ENTITY khachHang)
        {
            var khachHangReturnFromSP= new KHACH_HANG_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("KHACH_HANG_LOGIN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = khachHang.EMAIL;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = khachHang.MAT_KHAU;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        khachHangReturnFromSP = HelperFunction.DataReaderMapToEntity<KHACH_HANG_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }

            KHACH_HANG_w_TOKEN userWithToken = null;

            if (khachHangReturnFromSP != null)
            {
                //RefreshToken refreshToken = GenerateRefreshToken();
                //user.RefreshTokens.Add(refreshToken);
                //await _context.SaveChangesAsync();

                userWithToken = new KHACH_HANG_w_TOKEN(khachHangReturnFromSP);
                //userWithToken.RefreshToken = refreshToken.Token;
            }

            if (userWithToken == null)
            {
                return NotFound();
            }

            //sign your token here here..
            //userWithToken.AccessToken = GenerateAccessToken(user.UserId);
            return userWithToken;
        }

    }
}
