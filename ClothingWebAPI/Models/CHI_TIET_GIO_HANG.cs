using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class CHI_TIET_GIO_HANG
    {
        public int ID_GH { get; set; }
        public int MA_CT_SP { get; set; }
        public int SO_LUONG { get; set; }
        public int GIA { get; set; }

        public GIO_HANG ID_GHNavigation { get; set; }
        public CHI_TIET_SAN_PHAM MA_CT_SPNavigation { get; set; }
    }
}
