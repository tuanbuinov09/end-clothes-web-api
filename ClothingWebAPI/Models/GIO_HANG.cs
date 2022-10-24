using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class GIO_HANG
    {
        public GIO_HANG()
        {
            CHI_TIET_GIO_HANG = new HashSet<CHI_TIET_GIO_HANG>();
        }

        public int ID_GH { get; set; }
        public string MA_KH { get; set; }
        public string HO_TEN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public DateTime? NGAY_GIAO { get; set; }
        public string DIA_CHI { get; set; }
        public string GHI_CHU { get; set; }
        public short? TRANG_THAI { get; set; }
        public string MA_NV_DUYET { get; set; }
        public string MA_NV_GIAO { get; set; }

        public KHACH_HANG MA_KHNavigation { get; set; }
        public NHAN_VIEN MA_NV_DUYETNavigation { get; set; }
        public NHAN_VIEN MA_NV_GIAONavigation { get; set; }
        public HOA_DON HOA_DON { get; set; }
        public ICollection<CHI_TIET_GIO_HANG> CHI_TIET_GIO_HANG { get; set; }
    }
}
