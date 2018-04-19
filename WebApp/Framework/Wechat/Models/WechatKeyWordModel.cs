using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatKeyWordModel
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("color")]
        public string HexColor { get; set; }

        public WechatKeyWordModel(string value, string hexcolor = "")
        {
            Value = value;
            HexColor = hexcolor;
        }
    }
}