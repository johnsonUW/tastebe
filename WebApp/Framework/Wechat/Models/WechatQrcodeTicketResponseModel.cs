using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatQrcodeTicketResponseModel : WechatErrorModel
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }
    }
}