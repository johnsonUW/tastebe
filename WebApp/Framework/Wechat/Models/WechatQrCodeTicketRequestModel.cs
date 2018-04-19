using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatQrCodeTicketRequestModel
    {
        [JsonProperty("action_name")]
        public string ActionName { get; set; }
        [JsonProperty("action_info")]
        public WechatQrTicketActionInfoModel ActionInfo { get; set; }
    }
}