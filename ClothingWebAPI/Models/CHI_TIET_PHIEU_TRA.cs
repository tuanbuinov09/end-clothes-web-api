using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class CHI_TIET_PHIEU_TRA
    {
        public string MA_PT { get; set; }
        public int MA_CT_SP { get; set; }
        public int? SO_LUONG { get; set; }

        public CHI_TIET_SAN_PHAM MA_CT_SPNavigation { get; set; }
        public PHIEU_TRA MA_PTNavigation { get; set; }
    }
}
