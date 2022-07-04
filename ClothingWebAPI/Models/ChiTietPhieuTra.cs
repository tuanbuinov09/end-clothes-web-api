using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class ChiTietPhieuTra
    {
        public string MaPt { get; set; }
        public string MaCtSp { get; set; }
        public int? SoLuong { get; set; }

        public ChiTietSanPham MaCtSpNavigation { get; set; }
        public PhieuTra MaPtNavigation { get; set; }
    }
}
