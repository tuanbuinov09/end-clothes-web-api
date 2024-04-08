namespace ClothingWebAPI.Entities
{
    public class SearchInputEntity
    {
        public int top { get; set; }

        public string keyword { get; set; }

        public int priceFrom { get; set; }

        public int priceTo { get; set; }
    }
}
