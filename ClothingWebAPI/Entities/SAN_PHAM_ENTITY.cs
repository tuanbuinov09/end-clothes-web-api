using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public class SAN_PHAM_ENTITY
    {
        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public string MA_TL { get; set; }
        public int? LUOT_XEM { get; set; }
        public int? TONG_SL_TON { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public string HINH_ANH { get; set; }
        public string MO_TA { get; set; }
        public string TEN_TL { get; set; }
        public string SIZE_STR { get; set; }
        public string GIA_STR { get; set; }
        public int PHAN_TRAM_GIAM { get; set; }
        public string GIA_STR_DA_GIAM { get; set; }
        public int TONG_SL_DA_BAN { get; set; }
        public List<CHI_TIET_SAN_PHAM_ENTITY> chiTietSanPham { get; set; }
        public List<HINH_ANH_SAN_PHAM_ENTITY> hinhAnhSanPham { get; set; }
        public string MA_NV { get; set; }
    }
}
