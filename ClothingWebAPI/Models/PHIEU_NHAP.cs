using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class PHIEU_NHAP
    {
        public PHIEU_NHAP()
        {
            CHI_TIET_PHIEU_NHAP = new HashSet<CHI_TIET_PHIEU_NHAP>();
        }

        public string MA_PN { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public string MA_NV { get; set; }

        public NHAN_VIEN MA_NVNavigation { get; set; }
        public ICollection<CHI_TIET_PHIEU_NHAP> CHI_TIET_PHIEU_NHAP { get; set; }
    }
}
