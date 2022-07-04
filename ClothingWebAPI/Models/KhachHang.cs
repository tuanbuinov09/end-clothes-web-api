using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            GioHang = new HashSet<GioHang>();
        }

        public string MaKh { get; set; }
        public string HoTen { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string MaSoThue { get; set; }
        public string MaTk { get; set; }

        public TaiKhoan MaTkNavigation { get; set; }
        public ICollection<GioHang> GioHang { get; set; }
    }
}
