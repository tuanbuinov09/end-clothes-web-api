using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class YEU_THICH_SAN_PHAM
    {
        public string MA_KH { get; set; }
        public string MA_SP { get; set; }
        public DateTime NGAY_THEM { get; set; }

        public KHACH_HANG MA_KHNavigation { get; set; }
        public SAN_PHAM MA_SPNavigation { get; set; }
    }
}
