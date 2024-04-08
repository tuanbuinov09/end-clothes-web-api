namespace ClothingWebAPI.Models
{
    public partial class HINH_ANH_SAN_PHAM
    {
        public string MA_SP { get; set; }
        public string MA_MAU { get; set; }
        public string HINH_ANH { get; set; }
        public BANG_MAU MA_MAUNavigation { get; set; }
        public SAN_PHAM MA_SPNavigation { get; set; }
    }
}
