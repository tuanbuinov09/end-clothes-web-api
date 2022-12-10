using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public partial class NHAN_VIEN_ENTITY
    {
        public NHAN_VIEN_ENTITY()
        {
            
        }

        public string MA_NV { get; set; }
        public string HO_TEN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string DIA_CHI { get; set; }
        public string CMND { get; set; }
        public string MA_TK { get; set; }
        public string MAT_KHAU { get; set; }
        public string MA_QUYEN { get; set; }
        public string TEN_QUYEN { get; set; }
        public bool TRANG_THAI { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public int SO_GH_NV_DANG_GIAO { get; set; }
        public string accessToken { get; set; }
    }
}
