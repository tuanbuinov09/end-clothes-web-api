using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class DANH_GIA_SAN_PHAM_ENTITY
    {
        public int MA_CT_DH { get; set; }
        public int ID_DH { get; set; }
        public int MA_CT_SP { get; set; }
        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public string MA_KH { get; set; }
        public string EMAIL { get; set; }
        public string TEN_KH { get; set; }
        public string NOI_DUNG { get; set; }
        public int DANH_GIA { get; set; }

        public DateTime? NGAY_DANH_GIA { get; set; }
    }
}
