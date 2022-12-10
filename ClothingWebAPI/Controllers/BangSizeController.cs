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
using Microsoft.AspNetCore.Authorization;
using ClothingWebAPI.Entities;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BangSizeController : ControllerBase
    {
        private readonly ILogger<BangSizeController> _logger;

        private readonly IConfiguration _configuration;
        public BangSizeController(IConfiguration configuration, ILogger<BangSizeController> logger)
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

        [HttpGet]
        [Route("all")]
        public async Task<IList<BANG_SIZE_ENTITY>> GetAll2()
        {
            List<BANG_SIZE_ENTITY> listSize = new List<BANG_SIZE_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_SIZE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        listSize = HelperFunction.DataReaderMapToList<BANG_SIZE_ENTITY>(reader);

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
            return listSize;
        }
        [HttpGet]
        public async Task<BANG_SIZE_ENTITY> GetById2([FromQuery(Name = "sizeId")] string sizeId)
        {
            BANG_SIZE_ENTITY bangSizeEntity = new BANG_SIZE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_SIZE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_SIZE", SqlDbType.VarChar).Value = sizeId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        bangSizeEntity = HelperFunction.DataReaderMapToEntity<BANG_SIZE_ENTITY>(reader);

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
            return bangSizeEntity;
        }

        
        [Authorize]
        [HttpPost]
        [Route("add")]
        public RESPONSE_ENTITY InsertSize([FromBody] BANG_SIZE_ENTITY bangSize)
        {
            var response = new RESPONSE_ENTITY();
         
                //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
                {
                    // Use count to get all available items before the connection closes
                    using (SqlCommand cmd = new SqlCommand("THEM_SIZE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@TEN_SIZE", SqlDbType.NVarChar).Value = bangSize.TEN_SIZE;
                        cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = bangSize.MA_NV;
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
        public RESPONSE_ENTITY EditSize([FromBody] BANG_SIZE_ENTITY bangSize)
        {
            var response = new RESPONSE_ENTITY();

            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("SUA_SIZE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_SIZE", SqlDbType.VarChar).Value = bangSize.MA_SIZE;
                    cmd.Parameters.Add("@TEN_SIZE", SqlDbType.NVarChar).Value = bangSize.TEN_SIZE;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = bangSize.MA_NV;

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
        public RESPONSE_ENTITY DeleteSize([FromQuery] string sizeId)
        {
            var response = new RESPONSE_ENTITY();

            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("XOA_SIZE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_SIZE", SqlDbType.VarChar).Value = sizeId;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, BANG_SIZE bangSize)
        {
            if (id != bangSize.MA_SIZE)
            {
                return BadRequest();
            }

            using (var db = new CLOTHING_STOREContext())
            {
                db.Entry(bangSize).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BANG_SIZE_exist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BANG_SIZE>> DeletePublisher(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var bangSize = await db.BANG_SIZE.FindAsync(id);
                if (bangSize == null)
                {
                    return NotFound();
                }

                db.BANG_SIZE.Remove(bangSize);
                await db.SaveChangesAsync();

                return bangSize;
            }
               
        }
        private bool BANG_SIZE_exist(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                return db.BANG_SIZE.Any(e => e.MA_SIZE == id); 
            }
        }
    }
}
