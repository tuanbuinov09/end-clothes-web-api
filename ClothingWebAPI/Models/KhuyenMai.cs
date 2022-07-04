using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class KhuyenMai
    {
        public KhuyenMai()
        {
            ChiTietKhuyenMai = new HashSet<ChiTietKhuyenMai>();
        }

        public string MaKm { get; set; }
        public string MaNv { get; set; }
        public DateTime? NgayApDung { get; set; }

        public NhanVien MaNvNavigation { get; set; }
        public ICollection<ChiTietKhuyenMai> ChiTietKhuyenMai { get; set; }
    }
}
