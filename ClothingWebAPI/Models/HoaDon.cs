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
        public int IdGh { get; set; }
        public DateTime NgayTao { get; set; }

        public GioHang IdGhNavigation { get; set; }
        public ICollection<PhieuTra> PhieuTra { get; set; }
    }
}
