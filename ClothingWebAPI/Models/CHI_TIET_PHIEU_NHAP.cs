namespace ClothingWebAPI.Models
{
    public partial class CHI_TIET_PHIEU_NHAP
    {
        public string MA_PN { get; set; }
        public int MA_CT_SP { get; set; }
        public int? SO_LUONG { get; set; }
        public int? GIA { get; set; }
        public CHI_TIET_SAN_PHAM MA_CT_SPNavigation { get; set; }
        public PHIEU_NHAP MA_PNNavigation { get; set; }
    }
}
