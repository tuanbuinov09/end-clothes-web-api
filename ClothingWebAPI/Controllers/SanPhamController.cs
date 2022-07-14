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
        private List<SAN_PHAM> toBeRemovedFromResult = new List<SAN_PHAM>();


        [HttpGet]
        [Route("")]
        public IEnumerable<SAN_PHAM> GetAll()
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM).Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI).OrderByDescending(sanPham => sanPham.NGAY_TAO).ToList();
                return listSanPham;

            }
            return null;
        }

        [HttpGet]
        [Route("ById")]
        public IEnumerable<SAN_PHAM> GetOne([FromQuery(Name = "productId")] string productId)
        {
            if (productId != null)
            {
                using (var db = new CLOTHING_STOREContext())
                {
                    var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM)
                        .Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI)
                        .ThenInclude(ctkm=>ctkm.MA_KMNavigation).Where(sanPham => sanPham.MA_SP == productId).ToList();
                    // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
                    foreach (var a in listSanPham.ToList())
                    {
                        foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
                        {
                            if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
                            {
                                System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
                                if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
                                {
                                    continue;
                                }
                                else
                                {
                                    a.CHI_TIET_KHUYEN_MAI.Remove(b);
                                    continue;
                                }
                            }
                        }
                    }

                    return listSanPham;

                }
            }

            return null;
        }
        [HttpGet]
        [Route("new-arrivals")]
        public IEnumerable<SAN_PHAM> GetNewArrivals([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        {
           
            if (offset != null & limit != null)
            {
                using (var db = new CLOTHING_STOREContext())
                {
                    var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM)
                        .Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI)
                        .ThenInclude(ctkm => ctkm.MA_KMNavigation)
                        .OrderByDescending(sanPham => sanPham.NGAY_TAO).Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();

                    // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
                    foreach (var a in listSanPham.ToList())
                    {
                        foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
                        {
                            if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
                            {
                                System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
                                if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
                                {
                                    continue;
                                }
                                else
                                {
                                    a.CHI_TIET_KHUYEN_MAI.Remove(b);
                                    continue;
                                }
                            }
                        }
                    }
                    return listSanPham;

                }
            }
            using (var db = new CLOTHING_STOREContext())
            {
                var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM).Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI).OrderByDescending(sanPham => sanPham.NGAY_TAO).ToList();
                // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
                foreach (var a in listSanPham.ToList())
                {
                    foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
                    {
                        if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
                        {
                            System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
                            if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
                            {
                                continue;
                            }
                            else
                            {
                                a.CHI_TIET_KHUYEN_MAI.Remove(b);
                                continue;
                            }
                        }
                    }
                }
                return listSanPham;

            }
            return null;
        }

        [HttpGet]
        [Route("most-viewed")]
        public IEnumerable<SAN_PHAM> GetMostViewed([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        {
            if (offset != null & limit != null)
            {
                using (var db = new CLOTHING_STOREContext())
                {
                    var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM).Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI).ThenInclude(ctkm => ctkm.MA_KMNavigation).OrderByDescending(sanPham => sanPham.LUOT_XEM).Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();
                    // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
                    foreach (var a in listSanPham.ToList())
                    {
                        foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
                        {
                            if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
                            {
                                System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
                                if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
                                {
                                    continue;
                                }
                                else
                                {
                                    a.CHI_TIET_KHUYEN_MAI.Remove(b);
                                    continue;
                                }
                            }
                        }
                    }

                    return listSanPham;

                }
            }
            using (var db = new CLOTHING_STOREContext())
            {
                var listSanPham = db.SAN_PHAM
                    .Include(sanPham => sanPham.CHI_TIET_SAN_PHAM)
                    .Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI)
                    .ThenInclude(ctkm => ctkm.MA_KMNavigation).OrderByDescending(sanPham => sanPham.LUOT_XEM).ToList();
                // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
                foreach (var a in listSanPham.ToList())
                {
                    foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
                    {
                        if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
                        {
                            System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
                            if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
                            {
                                continue;
                            }
                            else
                            {
                                a.CHI_TIET_KHUYEN_MAI.Remove(b);
                                continue;
                            }
                        }
                    }
                }
                return listSanPham;

            }

            return null;
        }

        [HttpGet]
        [Route("sale-off")]
        // lấy tất cả sản phẩm đang khuyến mãi
        public IEnumerable<SAN_PHAM> GetSaleOffProduct([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        {
            DateTime start = DateTime.Now.Date;
            //var end = start.AddDays(1);

            //Where(x => x.CreatedDate >= start && x.CreatedDate < end)
            
            if (offset != null & limit != null)
            {
                using (var db = new CLOTHING_STOREContext())
                {
                    var listSanPhamKhuyenMai = db.SAN_PHAM.Include(sanpham => sanpham.CHI_TIET_SAN_PHAM)
                        .Include(sanpham => sanpham.CHI_TIET_KHUYEN_MAI)
                        .ThenInclude(ctkm=>ctkm.MA_KMNavigation)
                        .Include(sanpham => sanpham.MA_TLNavigation).Skip(int.Parse(offset)).Take(int.Parse(limit))
                        .OrderByDescending(sanPham => sanPham.LUOT_XEM).ToList();
                    foreach (var a in listSanPhamKhuyenMai.ToList())
                    {
                       foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
                        {
                            if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
                            {
                                System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN))>=DateTime.Now));
                                if((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
                                {
                                    continue;
                                }
                                else
                                {
                                    a.CHI_TIET_KHUYEN_MAI.Remove(b);
                                    continue;
                                }
                            }
                        }
                    }

                    toBeRemovedFromResult.Clear();
                    foreach (var a in listSanPhamKhuyenMai)
                    {
                        if (!(a.CHI_TIET_KHUYEN_MAI.Count > 0))
                        {
                            toBeRemovedFromResult.Add(a);
                        }
                    }
                    foreach (var a in toBeRemovedFromResult)
                    {
                        listSanPhamKhuyenMai.Remove(a);
                    }
                    return listSanPhamKhuyenMai;

                }
            }
            using (var db = new CLOTHING_STOREContext())
            {
                var listSanPhamKhuyenMai = db.SAN_PHAM.Include(sanpham => sanpham.CHI_TIET_SAN_PHAM)
                        .Include(sanpham => sanpham.CHI_TIET_KHUYEN_MAI)
                        .ThenInclude(ctkm => ctkm.MA_KMNavigation)
                        .Include(sanpham => sanpham.MA_TLNavigation).ToList();
                foreach (var a in listSanPhamKhuyenMai.ToList())
                {
                    foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
                    {
                        if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
                        {
                            System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
                            if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
                            {
                                continue;
                            }
                            else
                            {
                                a.CHI_TIET_KHUYEN_MAI.Remove(b);
                                continue;
                            }
                        }
                    }
                }
                // dùng cách này tránh lỗi System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
                toBeRemovedFromResult.Clear();
                foreach (var a in listSanPhamKhuyenMai)
                {
                    if (!(a.CHI_TIET_KHUYEN_MAI.Count > 0))
                    {
                        toBeRemovedFromResult.Add(a);
                    }
                }
                foreach (var a in toBeRemovedFromResult)
                {
                    listSanPhamKhuyenMai.Remove(a);
                }
                return listSanPhamKhuyenMai;

            }

            return null;
        }
        //[HttpGet]
        //[Route("sale-off")]
        //public async Task<JsonResult> GetAsync()
        //{
        //    //...
        //    string query = @"EXEC DBO.LAY_TAT_CA_CT_SP_DANG_KHUYENMAI";
        //    DataTable dataTable = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("CLOTHING_STORE_CONN");
        //    SqlDataReader sqlDataReader;
        //    await using (SqlConnection myConn = new SqlConnection(sqlDataSource))
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
        //    var ress = new JsonResult(dataTable);
        //    return ress;
        //}
        private bool logDatetiem(DateTime a)
        {
            Console.WriteLine("--------" + a);
            return false;
        }
    }

    
}
