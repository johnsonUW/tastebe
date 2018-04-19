using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WeChatJsApiTicketModel : WechatErrorModel
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }
    }
}