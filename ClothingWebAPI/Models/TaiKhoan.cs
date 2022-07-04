using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class TaiKhoan
    {
        public string MaTk { get; set; }
        public string MatKhau { get; set; }
        public string MaQuyen { get; set; }

        public Quyen MaQuyenNavigation { get; set; }
        public KhachHang KhachHang { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
