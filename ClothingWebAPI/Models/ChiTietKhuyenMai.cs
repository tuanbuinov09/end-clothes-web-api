using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class ChiTietKhuyenMai
    {
        public string MaKm { get; set; }
        public string MaCtSp { get; set; }
        public short? PhanTramGiam { get; set; }

        public ChiTietSanPham MaCtSpNavigation { get; set; }
        public KhuyenMai MaKmNavigation { get; set; }
    }
}
