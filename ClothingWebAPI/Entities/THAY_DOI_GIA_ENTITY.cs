using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public partial class THAY_DOI_GIA_ENTITY
    {
        public THAY_DOI_GIA_ENTITY()
        {

        }
        public int MA_CT_SP { get; set; }
        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public string MA_SIZE { get; set; }
        public string MA_MAU { get; set; }
        public string TEN_MAU { get; set; }
        public string TEN_TIENG_ANH { get; set; }
        public DateTime? NGAY_THAY_DOI { get; set; }

        public string TEN_SIZE { get; set; }
        public int? GIA { get; set; }
        public int? GIA_THAY_DOI { get; set; }
        public string GHI_CHU { get; set; }
        public string MA_NV { get; set; }
        public string HO_TEN_NV { get; set; }
    }
}
