using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatQrCodeRequestModel
    {
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}