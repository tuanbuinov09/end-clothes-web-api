using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class TI_GIA
    {
        public string MA_NV { get; set; }
        public DateTime NGAY_AP_DUNG { get; set; }
        public int? TI_GIA1 { get; set; }

        public NHAN_VIEN MA_NVNavigation { get; set; }
    }
}
