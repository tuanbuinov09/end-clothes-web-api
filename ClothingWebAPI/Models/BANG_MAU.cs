using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class BANG_MAU
    {
        public BANG_MAU()
        {
            CHI_TIET_SAN_PHAM = new HashSet<CHI_TIET_SAN_PHAM>();
            HINH_ANH_SAN_PHAM = new HashSet<HINH_ANH_SAN_PHAM>();
        }
        public string MA_MAU { get; set; }
        public string TEN_MAU { get; set; }
        public string TEN_TIENG_ANH { get; set; }
        public ICollection<CHI_TIET_SAN_PHAM> CHI_TIET_SAN_PHAM { get; set; }
        public ICollection<HINH_ANH_SAN_PHAM> HINH_ANH_SAN_PHAM { get; set; }
    }
}
