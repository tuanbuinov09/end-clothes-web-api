using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            PhieuTra = new HashSet<PhieuTra>();
        }

        public string MaHd { get; set; }
        public string MaGh { get; set; }
        public DateTime NgayTao { get; set; }
        public string MaNvDuyet { get; set; }
        public string MaNvGiaoHang { get; set; }

        public GioHang MaGhNavigation { get; set; }
        public ICollection<PhieuTra> PhieuTra { get; set; }
    }
}
