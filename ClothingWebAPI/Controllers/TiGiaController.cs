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

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiGiaController : ControllerBase
    {
        private readonly ILogger<TiGiaController> _logger;

        private readonly IConfiguration _configuration;
        public TiGiaController(IConfiguration configuration, ILogger<TiGiaController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [Route("current")]
        [HttpGet]
        public int GetCurrentUSD_VNDRate()
        {
            var res = 0;
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {

                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TI_GIA_HIEN_TAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                res = (int)reader["TI_GIA"];
                            }
                        }
                    }
                    cmd.Connection.Close();
                }

            }
            return res;

        }
    }
}
