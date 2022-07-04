using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class ChiTietSanPham
    {
        public ChiTietSanPham()
        {
            ChiTietGioHang = new HashSet<ChiTietGioHang>();
            ChiTietKhuyenMai = new HashSet<ChiTietKhuyenMai>();
            ChiTietPhieuNhap = new HashSet<ChiTietPhieuNhap>();
            ChiTietPhieuTra = new HashSet<ChiTietPhieuTra>();
        }

        public string MaCtSp { get; set; }
        public string MaSp { get; set; }
        public string MaSize { get; set; }
        public string Gia { get; set; }
        public int? SlTon { get; set; }

        public BangSize MaSizeNavigation { get; set; }
        public SanPham MaSpNavigation { get; set; }
        public ICollection<ChiTietGioHang> ChiTietGioHang { get; set; }
        public ICollection<ChiTietKhuyenMai> ChiTietKhuyenMai { get; set; }
        public ICollection<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
        public ICollection<ChiTietPhieuTra> ChiTietPhieuTra { get; set; }
    }
}
