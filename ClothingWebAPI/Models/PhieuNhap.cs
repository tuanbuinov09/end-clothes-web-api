using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class PhieuNhap
    {
        public PhieuNhap()
        {
            ChiTietPhieuNhap = new HashSet<ChiTietPhieuNhap>();
        }

        public string MaPn { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MaNv { get; set; }

        public NhanVien MaNvNavigation { get; set; }
        public ICollection<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
    }
}
