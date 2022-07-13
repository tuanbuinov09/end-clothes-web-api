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
    public class KhuyenMaiController : ControllerBase
    {
        private readonly ILogger<KhuyenMaiController> _logger;

        private readonly IConfiguration _configuration;
        public KhuyenMaiController(IConfiguration configuration, ILogger<KhuyenMaiController> logger)
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
        [Route("")]
        public IEnumerable<KHUYEN_MAI> GetAll()
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var listKhuyenMai = db.KHUYEN_MAI.Include(khuyenMai => khuyenMai.CHI_TIET_KHUYEN_MAI).ThenInclude(chiTietKhuyenMai=>chiTietKhuyenMai.MA_SPNavigation)
                    .ThenInclude(sanPham=> sanPham.CHI_TIET_SAN_PHAM).OrderByDescending(khuyenMai => khuyenMai.NGAY_AP_DUNG).ToList();
                return listKhuyenMai;

            }
            return null;
        }
        [HttpGet]
        [Route("product")]
        public IEnumerable<KHUYEN_MAI> GetAllSaleOffProducts([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        {
            if (offset != null & limit != null)
            {
                using (var db = new CLOTHING_STOREContext())
                {
                   
                        var listKhuyenMai = db.KHUYEN_MAI.Include(khuyenMai => khuyenMai.CHI_TIET_KHUYEN_MAI)
                        .ThenInclude(chiTietKhuyenMai => chiTietKhuyenMai.MA_SPNavigation)
                        .ThenInclude(sanPham=>sanPham.CHI_TIET_SAN_PHAM)
                        //.Where(khuyenMai => DateTime.Compare(DateTime.Now, (DateTime) khuyenMai.NGAY_AP_DUNG)>=0 && (DateTime.Now - (DateTime)khuyenMai.NGAY_AP_DUNG).TotalHours <=khuyenMai.THOI_GIAN)
                        .OrderByDescending(khuyenMai => khuyenMai.NGAY_AP_DUNG)
                        .Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();
                        return listKhuyenMai;
                }
            }
            using (var db = new CLOTHING_STOREContext())
            {
                var listKhuyenMai = db.KHUYEN_MAI.Include(khuyenMai => khuyenMai.CHI_TIET_KHUYEN_MAI)
                     .ThenInclude(chiTietKhuyenMai => chiTietKhuyenMai.MA_SPNavigation)
                     .ThenInclude(sanPham => sanPham.CHI_TIET_SAN_PHAM)
                    //.Where(khuyenMai => DateTime.Compare(DateTime.Now, (DateTime)khuyenMai.NGAY_AP_DUNG) >= 0 && (DateTime.Now - (DateTime)khuyenMai.NGAY_AP_DUNG).TotalHours <= khuyenMai.THOI_GIAN)
                    .OrderByDescending(khuyenMai => khuyenMai.NGAY_AP_DUNG).ToList();
                return listKhuyenMai;

            }
            return null;
        }
        //[HttpGet]
        //[Route("ById")]
        //public IEnumerable<KhuyenMai> GetOne([FromQuery(Name = "maKhuyenMai")] string maKhuyenMai)
        //{
        //    if (maKhuyenMai != null)
        //    {
        //        using (var db = new CLOTHING_STOREContext())
        //        {
        //            var listKhuyenMai = db.KhuyenMai.Include(khuyenMai => khuyenMai.ChiTietKhuyenMai).ThenInclude(chiTietKhuyenMai => chiTietKhuyenMai).Where(khuyenMai=>khuyenMai.MaKm==maKhuyenMai);
        //            return listKhuyenMai;

        //        }
        //    }

        //    return null;
        //}
        //[HttpGet]
        //[Route("new-arrivals")]
        //public IEnumerable<SanPham> GetNewArrivals([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        //{
        //    if (offset != null & limit != null)
        //    {
        //        using (var db = new CLOTHING_STOREContext())
        //        {
        //            var listSanPham = db.SanPham.Include(sanPham => sanPham.ChiTietSanPham).ThenInclude(sanPham => sanPham.ChiTietKhuyenMai).OrderByDescending(sanPham => sanPham.NgayTao).Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();
        //            return listSanPham;

        //        }
        //    }
        //    using (var db = new CLOTHING_STOREContext())
        //    {
        //        var listSanPham = db.SanPham.Include(sanPham => sanPham.ChiTietSanPham).ThenInclude(sanPham => sanPham.ChiTietKhuyenMai).OrderByDescending(sanPham => sanPham.NgayTao).ToList();
        //        return listSanPham;

        //    }
        //    return null;
        //}

    }
}
