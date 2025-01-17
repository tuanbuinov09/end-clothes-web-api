﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class KHACH_HANG_w_TOKEN : KHACH_HANG_ENTITY
    {
        public KHACH_HANG_w_TOKEN(KHACH_HANG_ENTITY khachHang)
        {
            this.HO_TEN = khachHang.HO_TEN;
            this.MA_KH = khachHang.MA_KH;
            this.SDT = khachHang.SDT;
            this.EMAIL = khachHang.EMAIL;
            this.DIA_CHI = khachHang.DIA_CHI;
            this.MA_SO_THUE = khachHang.MA_SO_THUE;
            this.MA_TK = khachHang.MA_TK;
            this.MAT_KHAU = khachHang.MAT_KHAU;
            this.MA_QUYEN = khachHang.MA_QUYEN;
            this.TEN_QUYEN = khachHang.TEN_QUYEN;
            this.TRANG_THAI = khachHang.TRANG_THAI;
            this.NGAY_TAO = khachHang.NGAY_TAO;
        }

        public string accessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
