using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class ChiTietPhieuNhap
    {
        public string MaPn { get; set; }
        public string MaCtSp { get; set; }
        public int? SoLuong { get; set; }
        public int? Gia { get; set; }

        public ChiTietSanPham MaCtSpNavigation { get; set; }
        public PhieuNhap MaPnNavigation { get; set; }
    }
}
