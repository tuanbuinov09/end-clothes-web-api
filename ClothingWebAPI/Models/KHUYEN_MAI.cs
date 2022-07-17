using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class KHUYEN_MAI
    {
        public KHUYEN_MAI()
        {
            CHI_TIET_KHUYEN_MAI = new HashSet<CHI_TIET_KHUYEN_MAI>();
        }

        public string MA_KM { get; set; }
        public string MA_NV { get; set; }
        public DateTime? NGAY_AP_DUNG { get; set; }
        public int? THOI_GIAN { get; set; }
        public string MO_TA { get; set; }

        public NHAN_VIEN MA_NVNavigation { get; set; }
        public ICollection<CHI_TIET_KHUYEN_MAI> CHI_TIET_KHUYEN_MAI { get; set; }
    }
}
