using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class BangSize
    {
        public BangSize()
        {
            ChiTietSanPham = new HashSet<ChiTietSanPham>();
        }

        public string MaSize { get; set; }
        public string TenSize { get; set; }

        public ICollection<ChiTietSanPham> ChiTietSanPham { get; set; }
    }
}
