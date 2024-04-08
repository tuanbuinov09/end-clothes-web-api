using ClothingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheLoaiController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public IEnumerable<THE_LOAI> GetAll()
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var listTheLoai = db.THE_LOAI.ToList();
                return listTheLoai;
            }
        }

        [Route("product")]
        [HttpGet]
        public IEnumerable<SAN_PHAM> GetAllProductOfTheLoai([FromQuery(Name = "categoryId")] string categoryId)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var listSanPhamCuaTheLoai = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM)
                    .Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI)
                    .Where(sanPham => sanPham.MA_TL == categoryId || sanPham.MA_TLNavigation.MA_TL_CHA==categoryId).ToList();
                return listSanPhamCuaTheLoai;
            }
        }
    }
}
