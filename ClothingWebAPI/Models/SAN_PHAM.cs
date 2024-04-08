using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class SAN_PHAM
    {
        public SAN_PHAM()
        {
            CHI_TIET_KHUYEN_MAI = new HashSet<CHI_TIET_KHUYEN_MAI>();
            CHI_TIET_SAN_PHAM = new HashSet<CHI_TIET_SAN_PHAM>();
            DANH_GIA_SAN_PHAM = new HashSet<DANH_GIA_SAN_PHAM>();
            HINH_ANH_SAN_PHAM = new HashSet<HINH_ANH_SAN_PHAM>();
            YEU_THICH_SAN_PHAM = new HashSet<YEU_THICH_SAN_PHAM>();
        }

        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public string MA_TL { get; set; }
        public int LUOT_XEM { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public string HINH_ANH { get; set; }
        public string MO_TA { get; set; }
        public THE_LOAI MA_TLNavigation { get; set; }
        public ICollection<CHI_TIET_KHUYEN_MAI> CHI_TIET_KHUYEN_MAI { get; set; }
        public ICollection<CHI_TIET_SAN_PHAM> CHI_TIET_SAN_PHAM { get; set; }
        public ICollection<DANH_GIA_SAN_PHAM> DANH_GIA_SAN_PHAM { get; set; }
        public ICollection<HINH_ANH_SAN_PHAM> HINH_ANH_SAN_PHAM { get; set; }
        public ICollection<YEU_THICH_SAN_PHAM> YEU_THICH_SAN_PHAM { get; set; }
    }
}
