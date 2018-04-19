using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Framework.Clover
{
    public class CloverCreateOrderModel
    {
        [JsonProperty("total")]
        public int Total => LineItems.Sum(l => l.PriceInPennies * l.UnitQuantity);
        public List<CloverLineItemModel> LineItems { get; set; }
    }
}