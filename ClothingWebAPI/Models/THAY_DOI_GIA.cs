using System;

namespace ClothingWebAPI.Models
{
    public partial class THAY_DOI_GIA
    {
        public string MA_NV { get; set; }
        public int MA_CT_SP { get; set; }
        public DateTime NGAY_THAY_DOI { get; set; }
        public int GIA { get; set; }
        public CHI_TIET_SAN_PHAM MA_CT_SPNavigation { get; set; }
        public NHAN_VIEN MA_NVNavigation { get; set; }
    }
}
