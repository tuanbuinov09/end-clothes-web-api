﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class CHI_TIET_GIO_HANG_ENTITY
    {
        public int STT { get; set; }
        public int MA_CT_DH { get; set; }
        public int ID_DH { get; set; }
        public int MA_CT_SP { get; set; }
        public string MA_SP { get; set; }
        public string TEN_SP { get; set; }
        public string HINH_ANH { get; set; }
        public string MA_SIZE { get; set; }
        public string TEN_SIZE { get; set; }
        public string MA_MAU { get; set; }
        public string TEN_MAU { get; set; }
        public int? GIA { get; set; }
        public int SO_LUONG { get; set; }
        public int SO_LUONG_TON { get; set; }
        public int SL_DA_TRA { get; set; }
        public int SL_TRA { get; set; }
    }
}
