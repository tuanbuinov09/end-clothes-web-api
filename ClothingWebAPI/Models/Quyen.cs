using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class QUYEN
    {
        public QUYEN()
        {
            TAI_KHOAN = new HashSet<TAI_KHOAN>();
        }

        public string MA_QUYEN { get; set; }
        public string TEN_QUYEN { get; set; }

        public ICollection<TAI_KHOAN> TAI_KHOAN { get; set; }
    }
}
