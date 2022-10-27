using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class DANH_GIA_SAN_PHAM
    {
        public string MA_KH { get; set; }
        public string MA_SP { get; set; }
        public short DANH_GIA { get; set; }
        public string NOI_DUNG { get; set; }
        public DateTime NGAY_DANH_GIA { get; set; }

        public KHACH_HANG MA_KHNavigation { get; set; }
        public SAN_PHAM MA_SPNavigation { get; set; }
    }
}
