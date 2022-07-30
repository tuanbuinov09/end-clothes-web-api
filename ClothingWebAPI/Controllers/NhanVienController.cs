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
    public class NhanVienController : ControllerBase
    {
        private readonly ILogger<NhanVienController> _logger;
        private readonly JWTSettings _jwtsettings;
        private readonly IConfiguration _configuration;
        public NhanVienController(IConfiguration configuration, ILogger<NhanVienController> logger, IOptions<JWTSettings> jwtsettings)
        {
            _logger = logger;
            _configuration = configuration;
            _jwtsettings = jwtsettings.Value;
        }

        // POST: api/NhanVien
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<NHAN_VIEN_ENTITY>> Login(NHAN_VIEN_ENTITY nhanVien)
        {
            var nhanVienReturnFromSP= new NHAN_VIEN_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("NHAN_VIEN_LOGIN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = nhanVien.EMAIL;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = nhanVien.MAT_KHAU;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        nhanVienReturnFromSP = HelperFunction.DataReaderMapToEntity<NHAN_VIEN_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }

        
            return nhanVienReturnFromSP;
        }


        // GET: api/NhanVien/delivering
        [HttpGet]
        [Route("delivering")]
        public async Task<IList<NHAN_VIEN_ENTITY>> GetEmployeeAndNumberOfOrderDelivering()
        {
            var nhanVienReturnFromSP = new List<NHAN_VIEN_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_NHAN_VIEN_VA_SO_DON_DANG_GIAO", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        nhanVienReturnFromSP = HelperFunction.DataReaderMapToList<NHAN_VIEN_ENTITY>(reader);
                    }
                    cmd.Connection.Close();
                }
            }

            return nhanVienReturnFromSP;
        }
    }
}
