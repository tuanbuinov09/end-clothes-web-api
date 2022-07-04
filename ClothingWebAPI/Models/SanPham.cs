using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietSanPham = new HashSet<ChiTietSanPham>();
        }

        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public string MaTl { get; set; }
        public int? LuotXem { get; set; }
        public DateTime? NgayTao { get; set; }
        public string HinhAnh { get; set; }

        public TheLoai MaTlNavigation { get; set; }
        public ICollection<ChiTietSanPham> ChiTietSanPham { get; set; }
    }
}
