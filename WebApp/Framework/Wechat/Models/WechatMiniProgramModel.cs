using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatMiniProgramModel
    {
        [JsonProperty("appid")]
        public string AppId { get; set; }
        [JsonProperty("pagepath")]
        public string PagePath { get; set; }
    }
}