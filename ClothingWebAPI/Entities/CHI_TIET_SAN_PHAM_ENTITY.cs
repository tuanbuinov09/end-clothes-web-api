namespace ClothingWebAPI.Entities
{
    public partial class CHI_TIET_SAN_PHAM_ENTITY
    {
        public int ID_DH { get; set; }

        public int MA_CT_SP { get; set; }

        public string MA_SP { get; set; }

        public string MA_SIZE { get; set; }

        public string MA_MAU { get; set; }

        public string TEN_MAU { get; set; } 

        public string TEN_TIENG_ANH { get; set; }

        public string HINH_ANH { get; set; }

        public string TEN_SIZE { get; set; }

        public int? GIA { get; set; }

        public int SO_LUONG { get; set; }

        public int? SL_TON { get; set; }
    }
}
