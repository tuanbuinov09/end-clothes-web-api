using ClothingWebAPI.Entities;
using ClothingWebAPI.Helpers;
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
    public class NhapHangController : ControllerBase
    {
        private readonly ILogger<NhapHangController> _logger;

        private readonly IConfiguration _configuration;
        public NhapHangController(IConfiguration configuration, ILogger<NhapHangController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("all")]
        public IList<PHIEU_NHAP_ENTITY> GetAllPhieuNhap([FromQuery(Name = "filterState")] int filterState)
        {
            _logger.LogInformation("GetAllPhieuNhap");

            var listPhieuNhap = new List<PHIEU_NHAP_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_PHIEU_NHAP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@TRANG_THAI", SqlDbType.Int).Value = filterState;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listPhieuNhap = HelperFunction.DataReaderMapToList<PHIEU_NHAP_ENTITY>(reader).ToList();
                    }
                    cmd.Connection.Close();
                }
            }
            return listPhieuNhap;
        }

        [HttpGet]
        [Route("")]
        public PHIEU_NHAP_ENTITY GetOnePhieuNhap([FromQuery(Name = "importId")] string importId)
        {
            var phieuNhap = new PHIEU_NHAP_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_PHIEU_NHAP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_PN", SqlDbType.VarChar).Value = importId;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        phieuNhap = HelperFunction.DataReaderMapToEntity<PHIEU_NHAP_ENTITY>(reader);
                    }
                    cmd.Connection.Close();
                }

                using (SqlCommand cmd = new SqlCommand("LAY_CHI_TIET_MOT_PHIEU_NHAP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_PN", SqlDbType.VarChar).Value = importId;

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        try
                        {
                            phieuNhap.chiTietPhieuNhap = HelperFunction.DataReaderMapToList<CHI_TIET_SAN_PHAM_ENTITY>(reader).ToList();
                        }
                        catch (Exception ex)
                        {
                            Debug.Write(ex.Message);
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            return phieuNhap;
        }

        [Authorize]
        [HttpPost]
        [Route("add-product-import")]
        public ActionResult<RESPONSE_ENTITY> addProductImport([FromBody] PHIEU_NHAP_ENTITY phieuNhap)
        {
            // chuyển list thành xml string để sql có thể đọc, xem store THEM_SAN_PHAM để biết thêm chi tiết
            var listChiTietPhieuNhap_Xml = HelperFunction.ConvertObjectToXMLString(phieuNhap.chiTietPhieuNhap);
            var response = new RESPONSE_ENTITY();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("THEM_PHIEU_NHAP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@GHI_CHU", SqlDbType.NVarChar).Value = phieuNhap.GHI_CHU;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = phieuNhap.MA_NV;
                    cmd.Parameters.Add("@xml_LIST_CHI_TIET_PN_STR", SqlDbType.NVarChar).Value = listChiTietPhieuNhap_Xml;

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
