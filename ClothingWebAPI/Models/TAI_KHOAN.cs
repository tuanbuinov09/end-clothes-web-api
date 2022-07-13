using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class TAI_KHOAN
    {
        public string MA_TK { get; set; }
        public string MAT_KHAU { get; set; }
        public string MA_QUYEN { get; set; }

        public QUYEN MA_QUYENNavigation { get; set; }
        public KHACH_HANG KHACH_HANG { get; set; }
        public NHAN_VIEN NHAN_VIEN { get; set; }
    }
}
