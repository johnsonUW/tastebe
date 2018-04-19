using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WeChatSession : WechatErrorModel
    {
        [JsonProperty("openid")]
        public string OpenId { get; set; }
        [JsonProperty("session_key")]
        public string SessionKey { get; set; }
        [JsonProperty("unionid")]
        public string UnionId { get; set; }
    }
}