using ClosedXML.Excel;
using ClothingWebAPI.Entities;
using ClothingWebAPI.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhanVienController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly ILogger<NhanVienController> _logger;
        private readonly JWTSettings _jwtsettings;
        private readonly IConfiguration _configuration;
        public NhanVienController(IConfiguration configuration, ILogger<NhanVienController> logger, IOptions<JWTSettings> jwtsettings, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _logger = logger;
            _configuration = configuration;
            _jwtsettings = jwtsettings.Value;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public async Task<IList<NHAN_VIEN_ENTITY>> GetAllNhanVien()
        {
            var listNhanVien = new List<NHAN_VIEN_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_NHAN_VIEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listNhanVien = HelperFunction.DataReaderMapToList<NHAN_VIEN_ENTITY>(reader).ToList();

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
            return listNhanVien;
        }

        [HttpGet]
        [Authorize]
        [Route("")]
        public NHAN_VIEN_ENTITY GetOneNhanVien([FromQuery(Name = "employeeId")] string employeeId)
        {
            var nhanVien = new NHAN_VIEN_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_NHAN_VIEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = employeeId;//có thể null
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        nhanVien = HelperFunction.DataReaderMapToEntity<NHAN_VIEN_ENTITY>(reader);

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

            if (nhanVien != null)
            {
                
            }

            return nhanVien;
        }

        [Authorize]
        [HttpPut]
        [Route("deactivate")]
        public RESPONSE_ENTITY DeactivateNhanVienAccount([FromQuery(Name = "employeeId")] string employeeId)
        {
            var res = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("VO_HIEU_HOA_TAI_KHOAN_NHAN_VIEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = employeeId;//có thể null
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        res = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

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
            return res;
        }
        [Authorize]
        [HttpPut]
        [Route("activate")]
        public RESPONSE_ENTITY ActivateNhanVienAccount([FromQuery(Name = "employeeId")] string employeeId)
        {
            var res = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("KICH_HOAT_TAI_KHOAN_NHAN_VIEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = employeeId;//có thể null
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        res = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

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
            return res;
        }
        [Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<ActionResult<RESPONSE_ENTITY>> InsertNhanVien(NHAN_VIEN_ENTITY nhanVien)
        {
            var res = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("THEM_NHAN_VIEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = nhanVien.EMAIL;

                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = HelperFunction.ComputeHash(nhanVien.MAT_KHAU, "SHA512", null);
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = nhanVien.DIA_CHI;
                    cmd.Parameters.Add("@soDienThoai", SqlDbType.VarChar).Value = nhanVien.SDT;
                    cmd.Parameters.Add("@hoTen", SqlDbType.NVarChar).Value = nhanVien.HO_TEN;
                    cmd.Parameters.Add("@cmnd", SqlDbType.VarChar).Value = nhanVien.CMND;
                    cmd.Parameters.Add("@maQuyen", SqlDbType.VarChar).Value = nhanVien.MA_QUYEN;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        res = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }
            if (res != null)
            {
                //verify

                //nhanVienReturnFromSP.accessToken = jwtAuthenticationManager.authenticate(nhanVienReturnFromSP.EMAIL, nhanVienReturnFromSP.MAT_KHAU);
                //userWithToken.RefreshToken = refreshToken.Token;
            }

            return res;
        }
        [Authorize]
        [HttpPost]
        [Route("validate-add")]
        public async Task<ActionResult<NHAN_VIEN_ENTITY>> ValidateInsertNhanVien(NHAN_VIEN_ENTITY nhanVien)
        {
            var nhanVienReturnFromSP = new NHAN_VIEN_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("KIEM_TRA_THEM_NHAN_VIEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = nhanVien.EMAIL;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = nhanVien.SDT;
                    cmd.Parameters.Add("@cmnd", SqlDbType.VarChar).Value = nhanVien.CMND;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        nhanVienReturnFromSP = HelperFunction.DataReaderMapToEntity<NHAN_VIEN_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }

            if (nhanVienReturnFromSP != null)
            {

            }
            return nhanVienReturnFromSP;
        }
        [Authorize]
        [HttpPut]
        [Route("edit")]
        public async Task<ActionResult<RESPONSE_ENTITY>> EditNhanVien(NHAN_VIEN_ENTITY nhanVien)
        {
            var res = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("SUA_NHAN_VIEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = nhanVien.EMAIL;

                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = HelperFunction.ComputeHash(nhanVien.MAT_KHAU, "SHA512", null);
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = nhanVien.DIA_CHI;
                    cmd.Parameters.Add("@soDienThoai", SqlDbType.VarChar).Value = nhanVien.SDT;
                    cmd.Parameters.Add("@hoTen", SqlDbType.NVarChar).Value = nhanVien.HO_TEN;
                    cmd.Parameters.Add("@cmnd", SqlDbType.NVarChar).Value = nhanVien.CMND;
                    cmd.Parameters.Add("@maQuyen", SqlDbType.VarChar).Value = nhanVien.MA_QUYEN;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        res = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }
           
            return res;
        }
        [Authorize]
        [HttpPost]
        [Route("validate-edit")]
        public async Task<ActionResult<NHAN_VIEN_ENTITY>> ValidateEditNhanVien(NHAN_VIEN_ENTITY nhanVien)
        {
            var nhanVienReturnFromSP = new NHAN_VIEN_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("KIEM_TRA_SUA_NHAN_VIEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
         
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = nhanVien.SDT;
                    cmd.Parameters.Add("@cmnd", SqlDbType.VarChar).Value = nhanVien.CMND;
                    cmd.Parameters.Add("@maNhanVien", SqlDbType.VarChar).Value = nhanVien.MA_NV;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        nhanVienReturnFromSP = HelperFunction.DataReaderMapToEntity<NHAN_VIEN_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }

            if (nhanVienReturnFromSP != null)
            {

            }
            return nhanVienReturnFromSP;
        }
        [Authorize]
        [HttpPut]
        [Route("change-info")]
        public async Task<ActionResult<NHAN_VIEN_ENTITY>> ChangeInfo(NHAN_VIEN_ENTITY nhanVien)
        {
            var nhanVienReturnFromSP = new NHAN_VIEN_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("NHAN_VIEN_CAP_NHAT_THONG_TIN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = nhanVien.EMAIL;

                    //cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = HelperFunction.ComputeHash(nhanVien.MAT_KHAU, "SHA512", null);
                    cmd.Parameters.Add("@diaChi", SqlDbType.NVarChar).Value = nhanVien.DIA_CHI;
                    cmd.Parameters.Add("@soDienThoai", SqlDbType.VarChar).Value = nhanVien.SDT;
                    cmd.Parameters.Add("@hoTen", SqlDbType.NVarChar).Value = nhanVien.HO_TEN;
                    cmd.Parameters.Add("@maNhanVien", SqlDbType.VarChar).Value = nhanVien.MA_NV;
                    //cmd.Parameters.Add("@maTaiKhoan", SqlDbType.VarChar).Value = nhanVien.MA_TK;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        nhanVienReturnFromSP = HelperFunction.DataReaderMapToEntity<NHAN_VIEN_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }


            if (nhanVienReturnFromSP != null)
            {
            }

            return nhanVienReturnFromSP;
        }
        [Authorize]
        [HttpPost]
        [Route("validate-change-info")]
        public async Task<ActionResult<NHAN_VIEN_ENTITY>> ValidateChangeInfo(NHAN_VIEN_ENTITY nhanVien)
        {
            var nhanVienReturnFromSP = new NHAN_VIEN_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("KIEM_TRA_NHAN_VIEN_CAP_NHAT_THONG_TIN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = nhanVien.EMAIL;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = nhanVien.SDT;
                    cmd.Parameters.Add("@maNhanVien", SqlDbType.VarChar).Value = nhanVien.MA_NV;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        nhanVienReturnFromSP = HelperFunction.DataReaderMapToEntity<NHAN_VIEN_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }


            if (nhanVienReturnFromSP == null)
            {
                return NotFound();
            }


            return nhanVienReturnFromSP;
        }
        [Authorize]
        [HttpPut]
        [Route("change-password")]
        public async Task<ActionResult<NHAN_VIEN_ENTITY>> ChangePassword(NHAN_VIEN_ENTITY nhanVien)
        {
            var nhanVienReturnFromSP = new NHAN_VIEN_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("NHAN_VIEN_DOI_MAT_KHAU", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = HelperFunction.ComputeHash(nhanVien.MAT_KHAU, "SHA512", null);
                    cmd.Parameters.Add("@maNhanVien", SqlDbType.VarChar).Value = nhanVien.MA_NV;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        nhanVienReturnFromSP = HelperFunction.DataReaderMapToEntity<NHAN_VIEN_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }
            }

            if (nhanVienReturnFromSP != null)
            {

                nhanVienReturnFromSP.accessToken = jwtAuthenticationManager.authenticate(nhanVienReturnFromSP.EMAIL, nhanVienReturnFromSP.MAT_KHAU);
            }

            if (nhanVienReturnFromSP == null)
            {
                return NotFound();
            }

            //sign your token here here..
            //userWithToken.AccessToken = GenerateAccessToken(user.UserId);
            return nhanVienReturnFromSP;
        }
        [Authorize]
        [HttpPost]
        [Route("validate-old-password")]
        public async Task<ActionResult<NHAN_VIEN_ENTITY>> ValidateOldPassword(NHAN_VIEN_ENTITY nhanVien)
        {
            var nhanVienReturnFromSP = new NHAN_VIEN_ENTITY();

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

            if (nhanVienReturnFromSP != null)
            {
                //verify
                bool flag = HelperFunction.VerifyHash(nhanVien.MAT_KHAU, "SHA512", nhanVienReturnFromSP.MAT_KHAU);

                if (flag == false)
                {
                    nhanVienReturnFromSP.MAT_KHAU = "1";//mật khẩu cũ không đúng
                    return nhanVienReturnFromSP;
                }
                nhanVienReturnFromSP.MAT_KHAU = "0";//mật khẩu cũ đúng
                //userWithToken.RefreshToken = refreshToken.Token;

            }

            return nhanVienReturnFromSP;
        }

        // POST: api/NhanVien
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<NHAN_VIEN_ENTITY>> Login(NHAN_VIEN_ENTITY nhanVien)
        {
            NHAN_VIEN_ENTITY nhanVienReturnFromSP = null;
            string tmpPass = HelperFunction.ComputeHash(nhanVien.MAT_KHAU, "SHA512", null);

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
            if (nhanVienReturnFromSP != null)
            {
                //verify
                bool flag = HelperFunction.VerifyHash(nhanVien.MAT_KHAU, "SHA512", nhanVienReturnFromSP.MAT_KHAU);

                if (flag == false)
                {
                    return null;
                }

                nhanVienReturnFromSP.accessToken = jwtAuthenticationManager.authenticate(nhanVienReturnFromSP.EMAIL, nhanVienReturnFromSP.MAT_KHAU);
                //userWithToken.RefreshToken = refreshToken.Token;

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

        [HttpGet]
        [Route("delivering-by-emp")]
        public async Task<IList<GIO_HANG_ENTITY>> GetAllGioHangDangGiaoBoiNhanVien([FromQuery(Name = "deliverEmpId")] string MANV_GIAO)
        {
            var listGioHang = new List<GIO_HANG_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_GIO_HANG_NV_DANG_GIAO", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MANV_GIAO", SqlDbType.VarChar).Value = MANV_GIAO;//có thể null

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
        [Authorize]
        [HttpPut]
        [Route("finish-cart")]
        public async Task<ActionResult<RESPONSE_ENTITY>> finishCart(DUYET_GIAO_GH_ENTITY duyetGiao)
        {
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("HOANTAT_GH", con))
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
        // GET: api/NhanVien/dashboardchart
        [HttpGet]
        [Route("dashboardchart")]
        public async Task<IList<COL_CHART_DATA_ENTITY>> GetDataDashBoardChart([FromQuery(Name = "n")] int n)
        {
            var data = new List<COL_CHART_DATA_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("BAO_CAO_DOANH_THU_N_NGAY_GAN_NHAT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@N", SqlDbType.VarChar).Value = n;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        data = HelperFunction.DataReaderMapToList<COL_CHART_DATA_ENTITY>(reader);
                    }
                    cmd.Connection.Close();
                }
            }

            return data;
        }

        [HttpGet]
        [Route("statistic")]
        public async Task<STATISTIC_DATA_ENTITY> GetStatistic()
        {
            var data = new STATISTIC_DATA_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_THONG_TIN_THONG_KE_TONG_QUAN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        data = HelperFunction.DataReaderMapToEntity<STATISTIC_DATA_ENTITY>(reader);
                    }
                    cmd.Connection.Close();
                }
            }

            return data;
        }
        [Authorize]
        [HttpGet]
        [Route("report-sale")]
        public async Task<IList<COL_CHART_DATA_ENTITY>> baoCaoDoanhThuTungThangTheoKhoang([FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to)
        {
            var data = new List<COL_CHART_DATA_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("BAO_CAO_DOANH_THU_THEO_KHOANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = from;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = to;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        data = HelperFunction.DataReaderMapToList<COL_CHART_DATA_ENTITY>(reader);
                    }
                    cmd.Connection.Close();
                }
            }
            return data;
        }

        [Authorize]
        [HttpGet]
        [Route("report-profit")]
        public async Task<IList<COL_CHART_DATA_ENTITY>> baoCaoLoiNhuanTungThangTheoKhoang([FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to, [FromQuery(Name = "type")] string type)
        {
            var data = new List<COL_CHART_DATA_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))

            string spName = "BAO_CAO_LOI_NHUAN_THEO_KHOANG";
            if (type == "day")
            {
                spName = "BAO_CAO_LOI_NHUAN_THEO_NGAY";
            }
            if (type == "quarter")
            {
                spName = "BAO_CAO_LOI_NHUAN_THEO_QUY";
            }
            if (type == "year")
            {
                spName = "BAO_CAO_LOI_NHUAN_THEO_NAM";
            }
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand(spName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = from;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = to;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        data = HelperFunction.DataReaderMapToList<COL_CHART_DATA_ENTITY>(reader);
                    }
                    cmd.Connection.Close();
                }
            }
            return data;
        }
    }




    //    [HttpGet]
    //    [Route("report-sale")]
    //    public IActionResult GEtE([FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to)
    //    {
    //        var data = new List<COL_CHART_DATA_ENTITY>();
    //        //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
    //        using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
    //        {
    //            // Use count to get all available items before the connection closes
    //            using (SqlCommand cmd = new SqlCommand("BAO_CAO_DOANH_THU_THEO_KHOANG", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = from;
    //                cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = to;
    //                cmd.Connection.Open();

    //                using (SqlDataReader reader = cmd.ExecuteReader())
    //                {
    //                    // Map data to Order class using this way
    //                    data = HelperFunction.DataReaderMapToList<COL_CHART_DATA_ENTITY>(reader);
    //                }
    //                cmd.Connection.Close();
    //            }
    //        }
    //        var testdata = new List<Employee>()
    //     {
    //         new Employee(){ EmpID=101, EmpName="Johnny"},
    //         new Employee(){ EmpID=102, EmpName="Tom"},
    //         new Employee(){ EmpID=103, EmpName="Jack"},
    //         new Employee(){ EmpID=104, EmpName="Vivian"},
    //         new Employee(){ EmpID=105, EmpName="Edward"},
    //     };
    //        //using System.Data;
    //        DataTable dt = new DataTable("Grid");
    //        dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Tháng"),
    //                                 new DataColumn("Doanh thu") });

    //        foreach (var dat in data)
    //        {
    //            dt.Rows.Add(dat.THANG, dat.TONG_TRI_GIA);
    //        }

    //        SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(@"F:\thuc-tap\ClothingWebAPI\ClothingWebAPI\report\template.xlsx", true);
    //        WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
    //        WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

    //        Worksheet worksheet = worksheetPart.Worksheet;
    //        SheetData sheetData = worksheet.GetFirstChild<SheetData>();
    //        //char[] refrence = "BCDEFGH".ToCharArray();
    //        char[] refrence = "BC".ToCharArray(); //excel column BC

    //        foreach (var dat in data)
    //        {
    //            int skipRowIndex = 5;
    //            skipRowIndex += data.IndexOf(dat);
    //            Row row = new Row();
    //            //for(int i=0; i<2; i++)
    //            //{
    //            Cell cell = new Cell()
    //            {
    //                CellValue = new CellValue(dat.THANG),
    //                CellReference = refrence[0].ToString() + skipRowIndex,
    //                DataType = CellValues.String,
    //            };
    //            row.Append(cell);

    //            Cell cell2 = new Cell()
    //            {
    //                CellValue = new CellValue(dat.TONG_TRI_GIA.ToString()),
    //                CellReference = refrence[1].ToString() + skipRowIndex,
    //                DataType = CellValues.String,
    //            };
    //            row.Append(cell2);

    //            sheetData.Append(row);
    //        }
    //        workbookPart.Workbook.Save();
    //        spreadsheetDocument.Close();
    //        //using ClosedXML.Excel;
    //        using (XLWorkbook wb = new XLWorkbook())
    //        {
    //            wb.Worksheets.Add(dt);
    //            using (MemoryStream stream = new MemoryStream())
    //            {
    //                wb.SaveAs(stream);
    //                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BaoCaoDoanhThu.xlsx");
    //            }
    //        }
    //    }
    //}

    internal class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }

        public Employee()
        {

        }
    }
}
