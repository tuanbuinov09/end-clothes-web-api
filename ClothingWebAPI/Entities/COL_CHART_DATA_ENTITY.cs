using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class COL_CHART_DATA_ENTITY
    {

        public int TONG_TRI_GIA { get; set; }
        public string NGAY_STR { get; set; }
        public string NGAY { get; set; } //datepart day
        public string THANG { get; set; }
        public string QUY { get; set; } //datepart quarter
        public string NAM { get; set; } //datepart year
        public int TONG_DOANH_THU { get; set; }
        public int TONG_GIA_NHAP { get; set; }
        public int TONG_GIA_TRA { get; set; }
        public int TONG_LOI_NHUAN { get; set; }
        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public string TEN_TL { get; set; }
        public int SO_LUONG { get; set; }
        public int GIA_BAN_TRUNG_BINH { get; set; }
        public int GIA_NHAP_TRUNG_BINH { get; set; }
        public COL_CHART_DATA_ENTITY()
        {
            
        }

    }
}
