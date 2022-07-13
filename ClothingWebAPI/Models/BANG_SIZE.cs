using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class BANG_SIZE
    {
        public BANG_SIZE()
        {
            CHI_TIET_SAN_PHAM = new HashSet<CHI_TIET_SAN_PHAM>();
        }

        public string MA_SIZE { get; set; }
        public string TEN_SIZE { get; set; }

        public ICollection<CHI_TIET_SAN_PHAM> CHI_TIET_SAN_PHAM { get; set; }
    }
}
