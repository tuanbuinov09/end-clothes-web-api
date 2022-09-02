using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class HOA_DON
    {
        public HOA_DON()
        {
            PHIEU_TRA = new HashSet<PHIEU_TRA>();
        }

        public string MA_HD { get; set; }
        public int ID_GH { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public string MA_NV { get; set; }
        public GIO_HANG ID_GHNavigation { get; set; }
        public ICollection<PHIEU_TRA> PHIEU_TRA { get; set; }
    }
}
