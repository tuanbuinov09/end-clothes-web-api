using ClosedXML.Excel;
using ClothingWebAPI.Entities;
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
        [HttpPost]
        [Route("report-sale")]
        public IActionResult ExportToExcel(REPORT_REQUEST_ENTITY reqBody)
        {
            var data = new List<COL_CHART_DATA_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("BAO_CAO_DOANH_THU_THEO_KHOANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = reqBody.from;
                    cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = reqBody.to;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        data = HelperFunction.DataReaderMapToList<COL_CHART_DATA_ENTITY>(reader);
                    }
                    cmd.Connection.Close();
                }
            }
            var testdata = new List<Employee>()
         {
             new Employee(){ EmpID=101, EmpName="Johnny"},
             new Employee(){ EmpID=102, EmpName="Tom"},
             new Employee(){ EmpID=103, EmpName="Jack"},
             new Employee(){ EmpID=104, EmpName="Vivian"},
             new Employee(){ EmpID=105, EmpName="Edward"},
         };
            //using System.Data;
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("EmpID"),
                                     new DataColumn("EmpName") });

            foreach (var emp in testdata)
            {
                dt.Rows.Add(emp.EmpID, emp.EmpName);
            }
            //using ClosedXML.Excel;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }

    internal class Employee
    {
        public int EmpID {get;set;}
        public string EmpName { get; set; }

        public Employee()
        {

        }
    }
}
