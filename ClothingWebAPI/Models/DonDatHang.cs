using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class DonDatHang
    {
        public string MaDdh { get; set; }
        public string MaNcc { get; set; }
        public DateTime? NgayTao { get; set; }
        public string MaNv { get; set; }
    }
}
