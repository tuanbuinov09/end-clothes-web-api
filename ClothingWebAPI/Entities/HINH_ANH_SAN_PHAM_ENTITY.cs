using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Entities
{
    public partial class HINH_ANH_SAN_PHAM_ENTITY
    {
        public HINH_ANH_SAN_PHAM_ENTITY()
        {
          
        }
     
        public string MA_SP { get; set; }
        public string MA_MAU { get; set; }
        public string TEN_MAU { get; set; }
        public string TEN_TIENG_ANH { get; set; }
        public string HINH_ANH { get; set; }

    }
}
