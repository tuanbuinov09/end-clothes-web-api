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
using Microsoft.AspNetCore.Authorization;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GioHangController : ControllerBase
    {
        private readonly ILogger<SanPhamController> _logger;

        private readonly IConfiguration _configuration;
        public GioHangController(IConfiguration configuration, ILogger<SanPhamController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet]
        [Route("all")]
        public async Task<IList<GIO_HANG_ENTITY>> GetAllGioHang([FromQuery(Name = "filterState")] int filterState)
        {
            var listGioHang = new List<GIO_HANG_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_GIO_HANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = filterState;//có thể null

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

        [HttpGet]
        [Route("")]

        public GIO_HANG_ENTITY GetOneGioHang([FromQuery(Name = "cartId")] string cartId)
        {
            var gioHang = new GIO_HANG_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_GIO_HANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = int.Parse(cartId.Trim());//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        gioHang = HelperFunction.DataReaderMapToEntity<GIO_HANG_ENTITY>(reader);

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

                using (SqlCommand cmd = new SqlCommand("LAY_CHI_TIET_MOT_GIO_HANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = cartId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        try
                        {
                            gioHang.chiTietGioHang2 = HelperFunction.DataReaderMapToList<CHI_TIET_GIO_HANG_ENTITY>(reader).ToList();

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
            return gioHang;
        }

        [HttpGet]
        [Route("for-return")]
        public GIO_HANG_ENTITY GetOneGioHangAndDetailForReturn([FromQuery(Name = "cartId")] string cartId)
        {
            var gioHang = new GIO_HANG_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_GIO_HANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = int.Parse(cartId.Trim());//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        gioHang = HelperFunction.DataReaderMapToEntity<GIO_HANG_ENTITY>(reader);

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

                using (SqlCommand cmd = new SqlCommand("LAY_CHI_TIET_MOT_GIO_HANG_VA_SL_DA_TRA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = cartId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        try
                        {
                            gioHang.chiTietGioHang2 = HelperFunction.DataReaderMapToList<CHI_TIET_GIO_HANG_ENTITY>(reader).ToList();

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
            return gioHang;
        }
        // hiện gộp chung duyệt và giao cho nhân viên vận chuyển
        //[HttpPut]
        //[Route("approve")]
        //public async Task<ActionResult<RESPONSE_ENTITY>> ApproveCart(DUYET_GIAO_GH_ENTITY duyetGiao)
        //{
        //    var response = new RESPONSE_ENTITY();
        //    //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
        //    using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
        //    {
        //        // Use count to get all available items before the connection closes
        //        using (SqlCommand cmd = new SqlCommand("DUYET_GH", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = duyetGiao.ID_GH;
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
        [Authorize]
        [HttpPut]
        [Route("assign-delivery")]
        public async Task<ActionResult<RESPONSE_ENTITY>> AssignCart(DUYET_GIAO_GH_ENTITY duyetGiao)
        {
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("GIAO_GH_CHO_NV_GIAO", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = duyetGiao.ID_DH;
                    cmd.Parameters.Add("@MA_NV_GIAO", SqlDbType.VarChar).Value = duyetGiao.MA_NV_GIAO;
                    cmd.Parameters.Add("@MA_NV_DUYET", SqlDbType.VarChar).Value = duyetGiao.MA_NV_DUYET;

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
