using Newtonsoft.Json;

namespace Framework.Clover
{
    public class CloverLineItemModel
    {
        [JsonProperty("price")]
        public int PriceInPennies { get; set; }
        /// <summary>
        /// whether or not to print this item in receipt
        /// </summary>
        public bool Printed { get; set; }
        public string Name { get; set; }
        [JsonProperty("unitQty")]
        public int UnitQuantity { get; set; }
    }
}