using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class KHACH_HANG
    {
        public KHACH_HANG()
        {
            DANH_GIA_SAN_PHAM = new HashSet<DANH_GIA_SAN_PHAM>();
            GIO_HANG = new HashSet<GIO_HANG>();
            YEU_THICH_SAN_PHAM = new HashSet<YEU_THICH_SAN_PHAM>();
        }

        public string MA_KH { get; set; }
        public string HO_TEN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string DIA_CHI { get; set; }
        public string MA_SO_THUE { get; set; }
        public string MA_TK { get; set; }
        public TAI_KHOAN MA_TKNavigation { get; set; }
        public ICollection<DANH_GIA_SAN_PHAM> DANH_GIA_SAN_PHAM { get; set; }
        public ICollection<GIO_HANG> GIO_HANG { get; set; }
        public ICollection<YEU_THICH_SAN_PHAM> YEU_THICH_SAN_PHAM { get; set; }
    }
}
