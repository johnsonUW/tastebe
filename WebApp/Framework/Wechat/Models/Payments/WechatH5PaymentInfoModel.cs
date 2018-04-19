using Newtonsoft.Json;

namespace Framework.Wechat.Models.Payments
{
    public class WechatH5PaymentInfoModel
    {
        [JsonProperty("h5_info")]
        public WechatH5PaymentInfoInnerModel Info { get; set; }
    }
}