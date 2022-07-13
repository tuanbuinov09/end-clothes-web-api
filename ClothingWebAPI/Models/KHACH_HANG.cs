using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class KHACH_HANG
    {
        public KHACH_HANG()
        {
            GIO_HANG = new HashSet<GIO_HANG>();
        }

        public string MA_KH { get; set; }
        public string HO_TEN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string DIA_CHI { get; set; }
        public string MA_SO_THUE { get; set; }
        public string MA_TK { get; set; }

        public TAI_KHOAN MA_TKNavigation { get; set; }
        public ICollection<GIO_HANG> GIO_HANG { get; set; }
    }
}
