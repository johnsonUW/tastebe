using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatOauthAccessTokenModel : WechatAccessTokenModel
    {
        [JsonProperty("openid")]
        public string UserOpenId { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}