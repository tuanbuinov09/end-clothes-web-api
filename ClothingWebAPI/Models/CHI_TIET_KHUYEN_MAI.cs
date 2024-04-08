namespace ClothingWebAPI.Models
{
    public partial class CHI_TIET_KHUYEN_MAI
    {
        public string MA_KM { get; set; }
        public string MA_SP { get; set; }
        public short PHAN_TRAM_GIAM { get; set; }
        public KHUYEN_MAI MA_KMNavigation { get; set; }
        public SAN_PHAM MA_SPNavigation { get; set; }
    }
}
