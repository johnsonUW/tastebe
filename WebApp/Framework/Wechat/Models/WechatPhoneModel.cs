using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatPhoneModel
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}