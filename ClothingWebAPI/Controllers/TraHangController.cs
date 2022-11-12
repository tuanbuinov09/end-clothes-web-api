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
using System.Data.Common;
using System.Reflection;
using System.Diagnostics;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraHangController : ControllerBase
    {
        private readonly ILogger<TraHangController> _logger;

        private readonly IConfiguration _configuration;
        public TraHangController(IConfiguration configuration, ILogger<TraHangController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet]
        [Route("all")]
        public async Task<IList<PHIEU_TRA_ENTITY>> GetAllPhieuTra([FromQuery(Name = "filterState")] int filterState)
        {
            var listPhieuTra = new List<PHIEU_TRA_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_PHIEU_TRA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = filterState;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listPhieuTra = HelperFunction.DataReaderMapToList<PHIEU_TRA_ENTITY>(reader).ToList();

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
            return listPhieuTra;
        }

        [HttpGet]
        [Route("")]
        public PHIEU_TRA_ENTITY GetOnePhieuTra([FromQuery(Name = "returnId")] string returnId)
        {
            var phieuTra = new PHIEU_TRA_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_PHIEU_TRA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_PT", SqlDbType.VarChar).Value = returnId;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        phieuTra = HelperFunction.DataReaderMapToEntity<PHIEU_TRA_ENTITY>(reader);

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

                using (SqlCommand cmd = new SqlCommand("LAY_CHI_TIET_MOT_PHIEU_TRA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_PT", SqlDbType.VarChar).Value = returnId;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        try
                        {
                            phieuTra.chiTietPhieuTra = HelperFunction.DataReaderMapToList<CHI_TIET_GIO_HANG_ENTITY>(reader).ToList();

                        }
                        catch (Exception ex)
                        {
                            Debug.Write("catch truowngf hop ma k ton tai nen k the tim ctsp");
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
            return phieuTra;
        }
        [HttpPost]
        [Route("add-product-return")]
        public async Task<ActionResult<RESPONSE_ENTITY>> addProductReturn([FromBody] PHIEU_TRA_ENTITY phieuTra)
        {
            // chuyển list thành xml string để sql có thể đọc, xem store THEM_SAN_PHAM để biết thêm chi tiết
            var listChiTietPhieuTra_Xml = HelperFunction.ConvertObjectToXMLString(phieuTra.chiTietPhieuTra);
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("THEM_PHIEU_TRA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = phieuTra.ID_GH;
                    cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = phieuTra.GHI_CHU;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = phieuTra.MA_NV;
                    cmd.Parameters.Add("@xml_LIST_CHI_TIET_PT_STR", SqlDbType.NVarChar).Value = listChiTietPhieuTra_Xml;

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
        //[HttpPut]
        //[Route("assign-delivery")]
        //public async Task<ActionResult<RESPONSE_ENTITY>> AssignCart(DUYET_GIAO_GH_ENTITY duyetGiao)
        //{
        //    var response = new RESPONSE_ENTITY();
        //    //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        //    using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
        //    {
        //        // Use count to get all available items before the connection closes
        //        using (SqlCommand cmd = new SqlCommand("GIAO_GH_CHO_NV_GIAO", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = duyetGiao.ID_GH;
        //            cmd.Parameters.Add("@MA_NV_GIAO", SqlDbType.VarChar).Value = duyetGiao.MA_NV_GIAO;
        //            cmd.Parameters.Add("@MA_NV_DUYET", SqlDbType.VarChar).Value = duyetGiao.MA_NV_DUYET;

        //            cmd.Connection.Open();

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                // Map data to Order class using this way
        //                response = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

        //            }
        //            cmd.Connection.Close();
        //        }
        //    }

        //    return response;
        //}
    }

}
