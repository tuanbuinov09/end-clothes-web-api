﻿using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class ChiTietGioHang
    {
        public string MaGh { get; set; }
        public string MaCtSp { get; set; }
        public int? SoLuong { get; set; }
        public double? Gia { get; set; }

        public ChiTietSanPham MaCtSpNavigation { get; set; }
        public GioHang MaGhNavigation { get; set; }
    }
}
