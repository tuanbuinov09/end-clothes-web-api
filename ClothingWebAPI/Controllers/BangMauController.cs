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
using Microsoft.AspNetCore.Authorization;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BangMauController : ControllerBase
    {
        private readonly ILogger<BangMauController> _logger;

        private readonly IConfiguration _configuration;
        public BangMauController(IConfiguration configuration, ILogger<BangMauController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }



        [HttpGet]
        [Route("all")]
        public async Task<IList<BANG_MAU_ENTITY>> GetAll2()
        {
            List<BANG_MAU_ENTITY> listMAU = new List<BANG_MAU_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_MAU", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        listMAU = HelperFunction.DataReaderMapToList<BANG_MAU_ENTITY>(reader);

                        // Map data to Order class using this way
                        //listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();
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
            return listMAU;
        }
        [HttpGet]
        public async Task<BANG_MAU_ENTITY> GetById2([FromQuery(Name = "colorId")] string colorId)
        {
            BANG_MAU_ENTITY bangMauEntity = new BANG_MAU_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_MAU", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_MAU", SqlDbType.VarChar).Value = colorId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        bangMauEntity = HelperFunction.DataReaderMapToEntity<BANG_MAU_ENTITY>(reader);

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
            return bangMauEntity;
        }


        [Authorize]
        [HttpPost]
        [Route("add")]
        public RESPONSE_ENTITY InsertMAU([FromBody] BANG_MAU_ENTITY bangMau)
        {
            var response = new RESPONSE_ENTITY();

            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("THEM_MAU", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TEN_MAU", SqlDbType.NVarChar).Value = bangMau.TEN_MAU;
                    cmd.Parameters.Add("@TEN_TIENG_ANH", SqlDbType.VarChar).Value = bangMau.TEN_TIENG_ANH;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = bangMau.MA_NV;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        response = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

                        // Map data to Order class using this way
                        //listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();
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
        [Authorize]
        [HttpPut]
        [Route("edit")]
        public RESPONSE_ENTITY EditMAU([FromBody] BANG_MAU_ENTITY bangMau)
        {
            var response = new RESPONSE_ENTITY();

            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("SUA_MAU", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_MAU", SqlDbType.VarChar).Value = bangMau.MA_MAU;
                    cmd.Parameters.Add("@TEN_MAU", SqlDbType.NVarChar).Value = bangMau.TEN_MAU;
                    cmd.Parameters.Add("@TEN_TIENG_ANH", SqlDbType.VarChar).Value = bangMau.TEN_TIENG_ANH;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = bangMau.MA_NV;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        response = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }

            }
            return response;
        }
        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public RESPONSE_ENTITY DeleteMAU([FromQuery] string colorId)
        {
            var response = new RESPONSE_ENTITY();

            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("XOA_MAU", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_MAU", SqlDbType.VarChar).Value = colorId;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        response = HelperFunction.DataReaderMapToEntity<RESPONSE_ENTITY>(reader);

                    }
                    cmd.Connection.Close();
                }

            }
            return response;
        }
        private bool BANG_MAU_exist(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                return db.BANG_MAU.Any(e => e.MA_MAU == id); 
            }
        }
    }
}
