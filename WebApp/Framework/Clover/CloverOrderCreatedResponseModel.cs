using Newtonsoft.Json;

namespace Framework.Clover
{
    public class CloverOrderCreatedResponseModel
    {
        [JsonProperty("href")]
        public string Link { get; set; }
        public string Id { get; set; }
        public string Currency { get; set; }
        public int Total { get; set; }
        public bool TaxRemoved { get; set; }
        public long CreatedTime { get; set; }
        public long ClientCreatedTime { get; set; }
        public long ModifiedTime { get; set; }
    }
}