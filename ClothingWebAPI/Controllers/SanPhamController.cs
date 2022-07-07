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
    public class SanPhamController : ControllerBase
    {
        private readonly ILogger<SanPhamController> _logger;

        private readonly IConfiguration _configuration;
        public SanPhamController(IConfiguration configuration, ILogger<SanPhamController> logger)
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
        public IEnumerable<SanPham> GetOne([FromQuery(Name = "productId")] string productId)
        {
            if (productId != null)
            {
                using (var db = new CLOTHING_STOREContext())
                {
                    var listSanPham = db.SanPham.Include(sanPham => sanPham.ChiTietSanPham).Where(sanPham=>sanPham.MaSp==productId);
                    return listSanPham;

                }
            }
            using (var db = new CLOTHING_STOREContext())
            {
                var listSanPham = db.SanPham.Include(sanPham => sanPham.ChiTietSanPham).OrderByDescending(sanPham => sanPham.NgayTao).ToList();
                return listSanPham;

            }
            return null;
        }
        [HttpGet]
        [Route("new-arrivals")]
        public IEnumerable<SanPham> GetNewArrivals([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        {
            if (offset != null & limit != null)
            {
                using (var db = new CLOTHING_STOREContext())
                {
                    var listSanPham = db.SanPham.Include(sanPham => sanPham.ChiTietSanPham).OrderByDescending(sanPham => sanPham.NgayTao).Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();
                    return listSanPham;

                }
            }
            using (var db = new CLOTHING_STOREContext())
            {
                var listSanPham = db.SanPham.Include(sanPham => sanPham.ChiTietSanPham).OrderByDescending(sanPham => sanPham.NgayTao).ToList();
                return listSanPham;

            }
            return null;
        }
       
        [HttpGet]
        [Route("most-viewed")]
        public IEnumerable<SanPham> GetMostViewed([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        {
            if(offset!=null & limit != null)
            {
                using (var db = new CLOTHING_STOREContext())
                {
                    var listSanPham = db.SanPham.Include(sanPham => sanPham.ChiTietSanPham).OrderByDescending(sanPham => sanPham.LuotXem).Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();
                    return listSanPham;

                }
            }
            using (var db = new CLOTHING_STOREContext())
            {
                var listSanPham = db.SanPham.Include(sanPham => sanPham.ChiTietSanPham).OrderByDescending(sanPham => sanPham.LuotXem).ToList();
                return listSanPham;

            }

            return null;
        }
        //[HttpGet]
        //public async Task<IEnumerable<SanPham>> GetAsync()
        //{
        //    //...
        //    string query = @"Select * from SAN_PHAM";
        //    DataTable dataTable = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("CLOTHING_STORE_CONN");
        //    SqlDataReader sqlDataReader;
        //    await using (SqlConnection myConn = new SqlConnection(sqlDataSource))
        //    {
        //        using (SqlCommand myCommand = new SqlCommand(query, myConn))
        //        {
        //            sqlDataReader = myCommand.ExecuteReader();
        //            dataTable.Load(sqlDataReader);
        //            sqlDataReader.Close();
        //            myConn.Close();
        //        }
        //    }
        //    SanPham sp = new SanPham();
        //    sp.TenSp = "a";
        //    return (IEnumerable<SanPham>) sp;
        //}
    }
}
