using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class Quyen
    {
        public Quyen()
        {
            TaiKhoan = new HashSet<TaiKhoan>();
        }

        public string MaQuyen { get; set; }
        public string TenQuyen { get; set; }

        public ICollection<TaiKhoan> TaiKhoan { get; set; }
    }
}
