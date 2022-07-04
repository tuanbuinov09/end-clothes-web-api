using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class TheLoai
    {
        public TheLoai()
        {
            SanPham = new HashSet<SanPham>();
        }

        public string MaTl { get; set; }
        public string TenTl { get; set; }
        public short? CapTl { get; set; }
        public string MaTlCha { get; set; }

        public ICollection<SanPham> SanPham { get; set; }
    }
}
