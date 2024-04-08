using ClothingWebAPI.Helpers;
using ClothingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuyenController : ControllerBase
    {
        private readonly ILogger<QuyenController> _logger;

        private readonly IConfiguration _configuration;

        public QuyenController(IConfiguration configuration, ILogger<QuyenController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Route("all")]
        [HttpGet]
        public IList<QUYEN> GetAll()
        {
            var listQuyen = new List<QUYEN>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_QUYEN", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listQuyen = HelperFunction.DataReaderMapToList<QUYEN>(reader).ToList();

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

            return listQuyen;

        }
    }
}
