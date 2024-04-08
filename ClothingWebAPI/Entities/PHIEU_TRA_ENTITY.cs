using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public class PHIEU_TRA_ENTITY
    {
        public string MA_PT { get; set; }

        public int ID_DH { get; set; }

        public string MA_HD { get; set; }

        public string MA_KH { get; set; }

        public string HO_TEN_KH { get; set; }

        public string SDT_KH { get; set; }

        public int TONG_SO_LUONG { get; set; }

        public DateTime? NGAY_TAO { get; set; }

        public string GHI_CHU { get; set; }

        public string MA_NV { get; set; }

        public string HO_TEN_NV { get; set; }

        public List<CHI_TIET_GIO_HANG_ENTITY> chiTietPhieuTra { get; set; }
    }
}
