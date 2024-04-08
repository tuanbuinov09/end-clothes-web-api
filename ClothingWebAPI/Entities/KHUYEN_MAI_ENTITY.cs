using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public class KHUYEN_MAI_ENTITY
    {
        public string MA_KM { get; set; }

        public DateTime? NGAY_TAO { get; set; }

        public DateTime? NGAY_AP_DUNG { get; set; }

        public string GHI_CHU { get; set; }

        public string MA_CAC_SP { get; set; }

        public int THOI_GIAN { get; set; }

        public int SO_LUONG_SP { get; set; }

        public string MA_NV { get; set; }

        public string HO_TEN_NV { get; set; }

        public int DANG_KHUYEN_MAI { get; set; }

        public bool TRANG_THAI { get; set; }

        public List<CHI_TIET_KHUYEN_MAI_ENTITY> chiTietKhuyenMai { get; set; }
    }
}
