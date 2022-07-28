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
        [Route("")]
        public async Task<IList<GIO_HANG_ENTITY>> GetAllGioHang()
        {
            var listGioHang = new List<GIO_HANG_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_GIO_HANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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

    }

}
