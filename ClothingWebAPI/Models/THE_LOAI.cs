using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class THE_LOAI
    {
        public THE_LOAI()
        {
            SAN_PHAM = new HashSet<SAN_PHAM>();
        }

        public string MA_TL { get; set; }
        public string TEN_TL { get; set; }
        public short CAP_TL { get; set; }
        public string MA_TL_CHA { get; set; }
        public ICollection<SAN_PHAM> SAN_PHAM { get; set; }
    }
}
