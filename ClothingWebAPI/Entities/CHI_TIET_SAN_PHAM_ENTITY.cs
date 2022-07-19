using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public partial class CHI_TIET_SAN_PHAM_ENTITY
    {
        public CHI_TIET_SAN_PHAM_ENTITY()
        {
          
        }

        public int MA_CT_SP { get; set; }
        public string MA_SP { get; set; }
        public string MA_SIZE { get; set; }
        public string TEN_SIZE { get; set; }
        public int? GIA { get; set; }
        public int? SL_TON { get; set; }

    }
}
