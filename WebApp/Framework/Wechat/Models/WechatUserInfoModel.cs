using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatUserInfoModel : WechatErrorModel
    {
        [JsonProperty("nickname")]
        public string Nickname { get; set; }
        [JsonProperty("sex")]
        public string Gender { get; set; }
        [JsonProperty("province")]
        public string Province { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("headimgurl")]
        public string ImageUrl { get; set; }
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}