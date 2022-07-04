using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class PhieuTra
    {
        public PhieuTra()
        {
            ChiTietPhieuTra = new HashSet<ChiTietPhieuTra>();
        }

        public string MaPt { get; set; }
        public string MaHd { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MaNv { get; set; }

        public HoaDon MaHdNavigation { get; set; }
        public NhanVien MaNvNavigation { get; set; }
        public ICollection<ChiTietPhieuTra> ChiTietPhieuTra { get; set; }
    }
}
