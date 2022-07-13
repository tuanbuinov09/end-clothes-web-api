using System;
using System.Collections.Generic;

namespace ClothingWebAPI.Models
{
    public partial class CHI_TIET_SAN_PHAM
    {
        public CHI_TIET_SAN_PHAM()
        {
            CHI_TIET_GIO_HANG = new HashSet<CHI_TIET_GIO_HANG>();
            CHI_TIET_PHIEU_NHAP = new HashSet<CHI_TIET_PHIEU_NHAP>();
            CHI_TIET_PHIEU_TRA = new HashSet<CHI_TIET_PHIEU_TRA>();
        }

        public string MA_CT_SP { get; set; }
        public string MA_SP { get; set; }
        public string MA_SIZE { get; set; }
        public int? GIA { get; set; }
        public int? SL_TON { get; set; }

        public BANG_SIZE MA_SIZENavigation { get; set; }
        public SAN_PHAM MA_SPNavigation { get; set; }
        public ICollection<CHI_TIET_GIO_HANG> CHI_TIET_GIO_HANG { get; set; }
        public ICollection<CHI_TIET_PHIEU_NHAP> CHI_TIET_PHIEU_NHAP { get; set; }
        public ICollection<CHI_TIET_PHIEU_TRA> CHI_TIET_PHIEU_TRA { get; set; }
    }
}
