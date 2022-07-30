using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class CHI_TIET_GIO_HANG_ENTITY
    {
        public int ID_GH { get; set; }
        public int MA_CT_SP { get; set; }
        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public string HINH_ANH { get; set; }
        public string MA_SIZE { get; set; }
        public string TEN_SIZE { get; set; }
        public int? GIA { get; set; }
        public int SO_LUONG { get; set; }
    }
}
