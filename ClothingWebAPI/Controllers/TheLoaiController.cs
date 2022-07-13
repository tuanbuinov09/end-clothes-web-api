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
    public class TheLoaiController : ControllerBase
    {
        private readonly ILogger<TheLoaiController> _logger;

        private readonly IConfiguration _configuration;
        public TheLoaiController(IConfiguration configuration, ILogger<TheLoaiController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        [Route("")]
        [HttpGet]
        public IEnumerable<THE_LOAI> GetAll()
        {

            using (var db = new CLOTHING_STOREContext())
            {
                var listTheLoai = db.THE_LOAI.ToList();
                return listTheLoai;
            }

            return null;
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

            return null;
        }
    }
}
