using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class GIO_HANG_ENTITY
    {
            public GIO_HANG_ENTITY()
        {
        }
        public int ID_GH { get; set; }
        public string MA_KH { get; set; }
        public string HO_TEN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public string DIA_CHI { get; set; }
        public short? TRANG_THAI { get; set; }
        public string MA_NV_DUYET { get; set; }
        public string TEN_NV_DUYET { get; set; }
        public string MA_NV_GIAO { get; set; }
        public string TEN_NV_GIAO { get; set; }
        public List<CHI_TIET_SAN_PHAM_ENTITY> chiTietGioHang { get; set; }

        public List<CHI_TIET_GIO_HANG_ENTITY> chiTietGioHang2 { get; set; }
    }
}
