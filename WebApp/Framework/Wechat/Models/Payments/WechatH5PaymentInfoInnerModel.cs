using Newtonsoft.Json;

namespace Framework.Wechat.Models.Payments
{
    public class WechatH5PaymentInfoInnerModel
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("app_name")]
        public string AppName { get; set; }
        /// <summary>
        /// ios only
        /// </summary>
        [JsonProperty("bundle_id")]
        public string BundleId { get; set; }
        /// <summary>
        /// android only
        /// </summary>
        [JsonProperty("package_name")]
        public string PackageName { get; set; }
    }
}