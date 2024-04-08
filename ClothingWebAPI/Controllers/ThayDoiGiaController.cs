using ClothingWebAPI.Entities;
using Microsoft.AspNetCore.Authorization;
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
    public class ThayDoiGiaController : ControllerBase
    {
        private readonly ILogger<ThayDoiGiaController> _logger;

        private readonly IConfiguration _configuration;

        public ThayDoiGiaController(IConfiguration configuration, ILogger<ThayDoiGiaController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("all")]
        public IList<THAY_DOI_GIA_ENTITY> GetAllThayDoiGia([FromQuery(Name = "filterState")] int filterState)
        {
            _logger.LogInformation("Get All Price Change");

            var listThayDoiGia = new List<THAY_DOI_GIA_ENTITY>();

            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_THAY_DOI_GIA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listThayDoiGia = HelperFunction.DataReaderMapToList<THAY_DOI_GIA_ENTITY>(reader).ToList();

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

            return listThayDoiGia;
        }

        [Authorize]
        [HttpPost]
        [Route("add-price-change")]
        public ActionResult<RESPONSE_ENTITY> addPriceChange([FromBody] THAY_DOI_GIA_INPUT_ENTITY thayDoiGia)
        {
            // chuyển list thành xml string để sql có thể đọc, xem store THEM_SAN_PHAM để biết thêm chi tiết
            var listThayDoiGia_Xml = HelperFunction.ConvertObjectToXMLString(thayDoiGia.listThayDoiGia);
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("THEM_THAY_DOI_GIA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = thayDoiGia.MA_NV;
                    cmd.Parameters.Add("@xml_LIST_THAY_DOI_GIA_STR", SqlDbType.NVarChar).Value = listThayDoiGia_Xml;

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
