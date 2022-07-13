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
        }

        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public string MA_TL { get; set; }
        public int? LUOT_XEM { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public string HINH_ANH { get; set; }

        public THE_LOAI MA_TLNavigation { get; set; }
        public ICollection<CHI_TIET_KHUYEN_MAI> CHI_TIET_KHUYEN_MAI { get; set; }
        public ICollection<CHI_TIET_SAN_PHAM> CHI_TIET_SAN_PHAM { get; set; }
    }
}
