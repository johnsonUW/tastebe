namespace Framework.Clover
{
    public class CloverItemModel
    {
        public string Id { get; set; }
        public bool Hidden { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string PriceType { get; set; }
        public bool DefaultTaxRates { get; set; }
        public int Cost { get; set; }
        public bool IsRevenue { get; set; }
        public long ModifiedTime { get; set; }
    }
}