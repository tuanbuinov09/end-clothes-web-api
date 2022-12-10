using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public partial class KHACH_HANG_ENTITY
    {
        public KHACH_HANG_ENTITY()
        {
            
        }

        public string MA_KH { get; set; }
        public string HO_TEN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string DIA_CHI { get; set; }
        public string MA_SO_THUE { get; set; }
        public string MA_TK { get; set; }
        public string MAT_KHAU { get; set; }
        public string MA_QUYEN { get; set; }
        public string TEN_QUYEN { get; set; }
        public bool TRANG_THAI { get; set; }

             public DateTime? NGAY_TAO { get; set; }
    }
}
