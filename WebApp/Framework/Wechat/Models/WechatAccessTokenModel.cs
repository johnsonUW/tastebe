using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatAccessTokenModel : WechatErrorModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}