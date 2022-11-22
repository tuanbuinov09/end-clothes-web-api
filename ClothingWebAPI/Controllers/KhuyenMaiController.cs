using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using ClothingWebAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ClothingWebAPI.Entities;
using System.Diagnostics;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly ILogger<KhuyenMaiController> _logger;

        private readonly IConfiguration _configuration;
        public KhuyenMaiController(IConfiguration configuration, ILogger<KhuyenMaiController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }


        //[HttpGet]
        //public JsonResult Get()
        //{
        //    string query = @"Select * from SAN_PHAM";
        //    DataTable dataTable = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("CLOTHING_STORE_CONN");
        //    SqlDataReader sqlDataReader;
        //    using (SqlConnection myConn = new SqlConnection(sqlDataSource))
        //    {
        //        myConn.Open();
        //        using (SqlCommand myCommand = new SqlCommand(query, myConn))
        //        {
        //            sqlDataReader = myCommand.ExecuteReader();
        //            dataTable.Load(sqlDataReader);
        //            sqlDataReader.Close();
        //            myConn.Close();
        //        }
        //    }

        //    return new JsonResult(dataTable);
        //}
        //[HttpGet]
        //[Route("")]
        //public IEnumerable<KHUYEN_MAI> GetAll()
        //{
        //    using (var db = new CLOTHING_STOREContext())
        //    {
        //        var listKhuyenMai = db.KHUYEN_MAI.Include(khuyenMai => khuyenMai.CHI_TIET_KHUYEN_MAI).ThenInclude(chiTietKhuyenMai=>chiTietKhuyenMai.MA_SPNavigation)
        //            .ThenInclude(sanPham=> sanPham.CHI_TIET_SAN_PHAM).OrderByDescending(khuyenMai => khuyenMai.NGAY_AP_DUNG).ToList();
        //        return listKhuyenMai;

        //    }
        //    return null;
        //}
        //[HttpGet]
        //[Route("product")]
        //public IEnumerable<KHUYEN_MAI> GetAllSaleOffProducts([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        //{
        //    if (offset != null & limit != null)
        //    {
        //        using (var db = new CLOTHING_STOREContext())
        //        {
                   
        //                var listKhuyenMai = db.KHUYEN_MAI.Include(khuyenMai => khuyenMai.CHI_TIET_KHUYEN_MAI)
        //                .ThenInclude(chiTietKhuyenMai => chiTietKhuyenMai.MA_SPNavigation)
        //                .ThenInclude(sanPham=>sanPham.CHI_TIET_SAN_PHAM)
        //                //.Where(khuyenMai => DateTime.Compare(DateTime.Now, (DateTime) khuyenMai.NGAY_AP_DUNG)>=0 && (DateTime.Now - (DateTime)khuyenMai.NGAY_AP_DUNG).TotalHours <=khuyenMai.THOI_GIAN)
        //                .OrderByDescending(khuyenMai => khuyenMai.NGAY_AP_DUNG)
        //                .Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();
        //                return listKhuyenMai;
        //        }
        //    }
        //    using (var db = new CLOTHING_STOREContext())
        //    {
        //        var listKhuyenMai = db.KHUYEN_MAI.Include(khuyenMai => khuyenMai.CHI_TIET_KHUYEN_MAI)
        //             .ThenInclude(chiTietKhuyenMai => chiTietKhuyenMai.MA_SPNavigation)
        //             .ThenInclude(sanPham => sanPham.CHI_TIET_SAN_PHAM)
        //            //.Where(khuyenMai => DateTime.Compare(DateTime.Now, (DateTime)khuyenMai.NGAY_AP_DUNG) >= 0 && (DateTime.Now - (DateTime)khuyenMai.NGAY_AP_DUNG).TotalHours <= khuyenMai.THOI_GIAN)
        //            .OrderByDescending(khuyenMai => khuyenMai.NGAY_AP_DUNG).ToList();
        //        return listKhuyenMai;

        //    }
        //    return null;
        //}
        [HttpGet]
        [Route("all")]
        public async Task<IList<KHUYEN_MAI_ENTITY>> GetAllDotKhuyenMai([FromQuery(Name = "filterState")] int filterState)
        {
            var listKhuyenMai = new List<KHUYEN_MAI_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_DOT_KHUYEN_MAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = filterState;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listKhuyenMai = HelperFunction.DataReaderMapToList<KHUYEN_MAI_ENTITY>(reader).ToList();

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
            return listKhuyenMai;
        }
        [HttpPost]
        [Route("add-sale-off")]
        public async Task<ActionResult<RESPONSE_ENTITY>> addSaleOff([FromBody] KHUYEN_MAI_ENTITY khuyenMai)
        {
            // chuyển list thành xml string để sql có thể đọc, xem store THEM_SAN_PHAM để biết thêm chi tiết
            var listChiTietKhuyenMai_Xml = HelperFunction.ConvertObjectToXMLString(khuyenMai.chiTietKhuyenMai);
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("THEM_KHUYEN_MAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = khuyenMai.GHI_CHU;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = khuyenMai.MA_NV;
                    cmd.Parameters.Add("@NGAY_AP_DUNG", SqlDbType.DateTime).Value = khuyenMai.NGAY_AP_DUNG;
                    cmd.Parameters.Add("@THOI_GIAN", SqlDbType.Int).Value = khuyenMai.THOI_GIAN;
                    cmd.Parameters.Add("@xml_LIST_CHI_TIET_KM_STR", SqlDbType.NVarChar).Value = listChiTietKhuyenMai_Xml;

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

        [HttpPut]
        [Route("edit-sale-off")]
        public async Task<ActionResult<RESPONSE_ENTITY>> editSaleOff([FromBody] KHUYEN_MAI_ENTITY khuyenMai)
        {
            // chuyển list thành xml string để sql có thể đọc, xem store THEM_SAN_PHAM để biết thêm chi tiết
            var listChiTietKhuyenMai_Xml = HelperFunction.ConvertObjectToXMLString(khuyenMai.chiTietKhuyenMai);
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("SUA_KHUYEN_MAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MA_KM", SqlDbType.VarChar).Value = khuyenMai.MA_KM;
                    cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = khuyenMai.GHI_CHU;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = khuyenMai.MA_NV;
                    cmd.Parameters.Add("@NGAY_AP_DUNG", SqlDbType.DateTime).Value = khuyenMai.NGAY_AP_DUNG;
                    cmd.Parameters.Add("@THOI_GIAN", SqlDbType.Int).Value = khuyenMai.THOI_GIAN;
                    cmd.Parameters.Add("@xml_LIST_CHI_TIET_KM_STR", SqlDbType.NVarChar).Value = listChiTietKhuyenMai_Xml;

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

        [HttpGet]
        [Route("")]
        public KHUYEN_MAI_ENTITY GetOneDotKhuyenMai([FromQuery(Name = "saleOffId")] string saleOffId)
        {
            var dotKhuyenMai = new KHUYEN_MAI_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_DOT_KHUYEN_MAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_KM", SqlDbType.VarChar).Value = saleOffId;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        dotKhuyenMai = HelperFunction.DataReaderMapToEntity<KHUYEN_MAI_ENTITY>(reader);

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

                using (SqlCommand cmd = new SqlCommand("LAY_CHI_TIET_MOT_DOT_KHUYEN_MAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_KM", SqlDbType.VarChar).Value = saleOffId;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        try
                        {
                            dotKhuyenMai.chiTietKhuyenMai = HelperFunction.DataReaderMapToList<CHI_TIET_KHUYEN_MAI_ENTITY>(reader).ToList();

                        }
                        catch (Exception ex)
                        {
                            //Debug.Write("catch truowngf hop ma k ton tai nen k the tim ctsp");
                        }

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
            return dotKhuyenMai;
        }
        [HttpDelete]
        [Route("delete")]
        public RESPONSE_ENTITY DeleteDotKhuyenMai([FromQuery(Name = "saleOffId")] string saleOffId)
        {
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("XOA_DOT_KHUYEN_MAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_KM", SqlDbType.VarChar).Value = saleOffId;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        response = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

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
            return response;
        }
    }
}
