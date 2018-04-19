using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatQrTicketActionInfoModel
    {
        [JsonProperty("scene")]
        public WechatQrTicketSceneInfoModel Scene { get; set; }
    }
}