using ClothingWebAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

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
        public IList<PHIEU_TRA_ENTITY> GetAllPhieuTra([FromQuery(Name = "filterState")] int filterState)
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
                            Debug.Write(ex.Message);
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            return phieuTra;
        }
        [Authorize]
        [HttpPost]
        [Route("add-product-return")]
        public ActionResult<RESPONSE_ENTITY> AddProductReturn([FromBody] PHIEU_TRA_ENTITY phieuTra)
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

                    cmd.Parameters.Add("@ID_GH", SqlDbType.Int).Value = phieuTra.ID_DH;
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
    }
}
