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
        [HttpPost]
        [Route("sign-up")]
        public async Task<ActionResult<KHACH_HANG_w_TOKEN>> SignUp(KHACH_HANG_ENTITY khachHang)
        {
            var khachHangReturnFromSP = new KHACH_HANG_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("KHACH_HANG_SIGN_UP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = khachHang.EMAIL;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = khachHang.MAT_KHAU;
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = khachHang.DIA_CHI;
                    cmd.Parameters.Add("@soDienThoai", SqlDbType.VarChar).Value = khachHang.SDT;
                    cmd.Parameters.Add("@hoTen", SqlDbType.NVarChar).Value = khachHang.HO_TEN;
                    cmd.Parameters.Add("@maKhachHang", SqlDbType.VarChar).Value = khachHang.MA_KH;
                    cmd.Parameters.Add("@maTaiKhoan", SqlDbType.VarChar).Value = khachHang.MA_TK;
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
        [HttpPost]
        [Route("validate-sign-up")]
        public async Task<ActionResult<KHACH_HANG_w_TOKEN>> ValidateSignUp(KHACH_HANG_ENTITY khachHang)
        {
            var khachHangReturnFromSP = new KHACH_HANG_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("KIEM_TRA_KHACH_HANG_SIGN_UP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = khachHang.EMAIL;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = khachHang.SDT;
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
                userWithToken = new KHACH_HANG_w_TOKEN(khachHangReturnFromSP);
            }

            if (userWithToken == null)
            {
                return NotFound();
            }

            return userWithToken;
        }
        [HttpPost]
        [Route("add-cart")]
        public async Task<ActionResult<string>> TaoGioHang(GIO_HANG_ENTITY gioHang)
        {
            var returnstr = "";
            var idGioHang = -1;
            var khachHangReturnFromSP = new KHACH_HANG_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
  
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("THEM_GIO_HANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_KH", SqlDbType.VarChar).Value = gioHang.MA_KH;
                    cmd.Parameters.Add("@HO_TEN", SqlDbType.NVarChar).Value = gioHang.HO_TEN;
                    cmd.Parameters.Add("@SDT", SqlDbType.VarChar).Value = gioHang.SDT;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = gioHang.EMAIL;
                    cmd.Parameters.Add("@DIA_CHI", SqlDbType.NVarChar).Value = gioHang.DIA_CHI;
                    if (gioHang.GHI_CHU != null)
                    {
                        cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = gioHang.GHI_CHU;
                    }
                    else
                    {
                        cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = "";
                    }
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //// Map data to Order class using this way
                        //khachHangReturnFromSP = HelperFunction.DataReaderMapToEntity<KHACH_HANG_ENTITY>(reader);
                        
                            if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                idGioHang = (int) reader["ID_GIO_HANG"];
                            }
                        }
                    }
                    cmd.Connection.Close();
                }
            }


            foreach(var b in gioHang.chiTietGioHang)
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
                {

                    // Use count to get all available items before the connection closes
                    using (SqlCommand cmd = new SqlCommand("THEM_VAO_CHI_TIET_GIO_HANG", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ID_GIO_HANG", SqlDbType.Int).Value = idGioHang;
                        cmd.Parameters.Add("@MA_CT_SP", SqlDbType.Int).Value = b.MA_CT_SP;
                        cmd.Parameters.Add("@SO_LUONG", SqlDbType.Int).Value = b.SO_LUONG;
                        cmd.Parameters.Add("@GIA", SqlDbType.Int).Value = b.GIA;
                        cmd.Connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            //// Map data to Order class using this way
                            //khachHangReturnFromSP = HelperFunction.DataReaderMapToEntity<KHACH_HANG_ENTITY>(reader);

                            //if (reader.HasRows)
                            //{
                            //    while (reader.Read())
                            //    {
                            //        idGioHang = (int)reader["ID_GIO_HANG"];
                            //    }
                            //}
                        }
                        cmd.Connection.Close();
                    }
                }

            }
            returnstr = "Đặt hàng thành công";

            return returnstr;
            //KHACH_HANG_w_TOKEN userWithToken = null;

            //if (khachHangReturnFromSP != null)
            //{
            //    //RefreshToken refreshToken = GenerateRefreshToken();
            //    //user.RefreshTokens.Add(refreshToken);
            //    //await _context.SaveChangesAsync();

            //    userWithToken = new KHACH_HANG_w_TOKEN(khachHangReturnFromSP);
            //    //userWithToken.RefreshToken = refreshToken.Token;
            //}

            //if (userWithToken == null)
            //{
            //    return NotFound();
            //}

            //sign your token here here..
            //userWithToken.AccessToken = GenerateAccessToken(user.UserId);
            //return userWithToken;
        }

        [HttpGet]
        [Route("carts")]
        public async Task<IList<GIO_HANG_ENTITY>> GetAllGioHang([FromQuery(Name = "filterState")] int filterState, [FromQuery(Name = "customerId")] string customerId)
        {
            var listGioHang = new List<GIO_HANG_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_GIO_HANG_CUA_KH", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = filterState;//có thể null
                    cmd.Parameters.Add("@MA_KH", SqlDbType.VarChar).Value = customerId;//có thể null
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listGioHang = HelperFunction.DataReaderMapToList<GIO_HANG_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }
            }
            return listGioHang;
        }

        [HttpPut]
        [Route("cancel-cart")]
        public async Task<ActionResult<RESPONSE_ENTITY>> CancelCart(DUYET_GIAO_GH_ENTITY duyetGiao)
        {
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("HUY_GH", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = duyetGiao.ID_GH;
                    
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        response = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }

            return response;
        }
    }
}
