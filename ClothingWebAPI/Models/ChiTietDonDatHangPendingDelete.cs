using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class ChiTietDonDatHangPendingDelete
    {
        public string MaDdh { get; set; }
        public string MaCtSp { get; set; }
        public int? SoLuong { get; set; }
        public double? Gia { get; set; }
    }
}
