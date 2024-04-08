using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class PHIEU_TRA
    {
        public PHIEU_TRA()
        {
            CHI_TIET_PHIEU_TRA = new HashSet<CHI_TIET_PHIEU_TRA>();
        }

        public string MA_PT { get; set; }
        public string MA_HD { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public string MA_NV { get; set; }
        public HOA_DON MA_HDNavigation { get; set; }
        public NHAN_VIEN MA_NVNavigation { get; set; }
        public ICollection<CHI_TIET_PHIEU_TRA> CHI_TIET_PHIEU_TRA { get; set; }
    }
}
