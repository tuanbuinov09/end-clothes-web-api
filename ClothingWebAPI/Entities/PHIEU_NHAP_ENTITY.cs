using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public class PHIEU_NHAP_ENTITY
    {
        public string MA_PN { get; set; }
        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public int TONG_GIA_NHAP { get; set; }
        public int TONG_SO_LUONG { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public string GHI_CHU { get; set; }
        public string MA_NV { get; set; }
        public string HO_TEN_NV { get; set; }
        public List<CHI_TIET_SAN_PHAM_ENTITY> chiTietPhieuNhap { get; set; }
    }
}
