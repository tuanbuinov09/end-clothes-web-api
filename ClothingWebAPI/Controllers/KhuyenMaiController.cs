using ClothingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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

        [HttpGet]
        [Route("")]
        public IEnumerable<KHUYEN_MAI> GetAll()
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var listKhuyenMai = db.KHUYEN_MAI.Include(khuyenMai => khuyenMai.CHI_TIET_KHUYEN_MAI).ThenInclude(chiTietKhuyenMai => chiTietKhuyenMai.MA_SPNavigation)
                    .ThenInclude(sanPham => sanPham.CHI_TIET_SAN_PHAM).OrderByDescending(khuyenMai => khuyenMai.NGAY_AP_DUNG).ToList();
                return listKhuyenMai;
            }
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
                    .ThenInclude(sanPham => sanPham.CHI_TIET_SAN_PHAM)
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
        }
    }
}
