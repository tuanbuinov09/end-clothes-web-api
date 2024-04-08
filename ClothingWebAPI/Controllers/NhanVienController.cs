using ClothingWebAPI.Entities;
using ClothingWebAPI.Helpers;
using ClothingWebAPI.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhanVienController : ControllerBase
    {
        private readonly ILogger<NhanVienController> _logger;
        private readonly JWTSetting _jwtsettings;
        private readonly IConfiguration _configuration;
        public NhanVienController(IConfiguration configuration, ILogger<NhanVienController> logger, IOptions<JWTSetting> jwtsettings)
        {
            _logger = logger;
            _configuration = configuration;
            _jwtsettings = jwtsettings.Value;
        }

        // POST: api/NhanVien
        [HttpPost]
        [Route("login")]
        public ActionResult<NHAN_VIEN_ENTITY> Login(NHAN_VIEN_ENTITY nhanVien)
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

            return nhanVienReturnFromSP;
        }

        // GET: api/NhanVien/delivering
        [HttpGet]
        [Route("delivering")]
        public IList<NHAN_VIEN_ENTITY> GetEmployeeAndNumberOfOrderDelivering()
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
        public IList<GIO_HANG_ENTITY> GetAllGioHangDangGiaoBoiNhanVien([FromQuery(Name = "deliverEmpId")] string MANV_GIAO)
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
                    }
                    cmd.Connection.Close();
                }
            }
            return listGioHang;
        }

        [HttpPut]
        [Route("finish-cart")]
        public ActionResult<RESPONSE_ENTITY> finishCart(DUYET_GIAO_GH_ENTITY duyetGiao)
        {
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("HOANTAT_GH", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = duyetGiao.ID_DH;

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
        public IList<COL_CHART_DATA_ENTITY> GetDataDashBoardChart([FromQuery(Name = "n")] int n)
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
        public STATISTIC_DATA_ENTITY GetStatistic()
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

        [HttpGet]
        [Route("report-sale")]
        public IList<COL_CHART_DATA_ENTITY> baoCaoDoanhThuTungThangTheoKhoang([FromQuery(Name = "from")] DateTime from, [FromQuery(Name = "to")] DateTime to)
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
    }
}
