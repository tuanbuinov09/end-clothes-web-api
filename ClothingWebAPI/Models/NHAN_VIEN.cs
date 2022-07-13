using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class NHAN_VIEN
    {
        public NHAN_VIEN()
        {
            GIO_HANGMA_NV_DUYETNavigation = new HashSet<GIO_HANG>();
            GIO_HANGMA_NV_GIAONavigation = new HashSet<GIO_HANG>();
            KHUYEN_MAI = new HashSet<KHUYEN_MAI>();
            PHIEU_NHAP = new HashSet<PHIEU_NHAP>();
            PHIEU_TRA = new HashSet<PHIEU_TRA>();
        }

        public string MA_NV { get; set; }
        public string HO_TEN { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string DIA_CHI { get; set; }
        public string CMND { get; set; }
        public string MA_TK { get; set; }

        public TAI_KHOAN MA_TKNavigation { get; set; }
        public ICollection<GIO_HANG> GIO_HANGMA_NV_DUYETNavigation { get; set; }
        public ICollection<GIO_HANG> GIO_HANGMA_NV_GIAONavigation { get; set; }
        public ICollection<KHUYEN_MAI> KHUYEN_MAI { get; set; }
        public ICollection<PHIEU_NHAP> PHIEU_NHAP { get; set; }
        public ICollection<PHIEU_TRA> PHIEU_TRA { get; set; }
    }
}
