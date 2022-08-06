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

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoaDon : ControllerBase
    {
        private readonly ILogger<BangSizeController> _logger;

        private readonly IConfiguration _configuration;
        public HoaDon(IConfiguration configuration, ILogger<BangSizeController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ActionResult<HOA_DON>> GetHDFromID_GH([FromQuery(Name = "cartId")] int cartId)
        {
            HOA_DON hd = new HOA_DON();

            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_HOA_DON_THEO_ID_GH", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = cartId;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        hd  = HelperFunction.DataReaderMapToEntity<HOA_DON>(reader);

                    }
                    cmd.Connection.Close();
                }
            }
            return hd;
        }


    [HttpPost]
        public async Task<ActionResult<string>> Post(HOA_DON hoaDon)
        {


            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("TAO_HOA_DON", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = hoaDon.ID_GH;
                    cmd.Parameters.Add("@MA_HD", SqlDbType.VarChar).Value = hoaDon.MA_HD;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                    }
                    cmd.Connection.Close();
                }
            }
            return "";
        }

    }
}
