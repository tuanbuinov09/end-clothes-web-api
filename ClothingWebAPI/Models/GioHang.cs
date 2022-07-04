using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class GioHang
    {
        public GioHang()
        {
            ChiTietGioHang = new HashSet<ChiTietGioHang>();
        }

        public string MaGh { get; set; }
        public string MaKh { get; set; }
        public string HoTen { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public short? TrangThai { get; set; }
        public string MaNvDuyet { get; set; }
        public string MaNvGiao { get; set; }

        public KhachHang MaKhNavigation { get; set; }
        public NhanVien MaNvDuyetNavigation { get; set; }
        public NhanVien MaNvGiaoNavigation { get; set; }
        public HoaDon HoaDon { get; set; }
        public ICollection<ChiTietGioHang> ChiTietGioHang { get; set; }
    }
}
