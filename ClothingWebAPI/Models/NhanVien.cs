using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            GioHangMaNvDuyetNavigation = new HashSet<GioHang>();
            GioHangMaNvGiaoNavigation = new HashSet<GioHang>();
            KhuyenMai = new HashSet<KhuyenMai>();
            PhieuNhap = new HashSet<PhieuNhap>();
            PhieuTra = new HashSet<PhieuTra>();
        }

        public string MaNv { get; set; }
        public string HoTen { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string Cmnd { get; set; }
        public string MaTk { get; set; }

        public TaiKhoan MaTkNavigation { get; set; }
        public ICollection<GioHang> GioHangMaNvDuyetNavigation { get; set; }
        public ICollection<GioHang> GioHangMaNvGiaoNavigation { get; set; }
        public ICollection<KhuyenMai> KhuyenMai { get; set; }
        public ICollection<PhieuNhap> PhieuNhap { get; set; }
        public ICollection<PhieuTra> PhieuTra { get; set; }
    }
}
