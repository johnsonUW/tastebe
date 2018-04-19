using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatTemplateDataModel
    {
        [JsonProperty("first")]
        public WechatKeyWordModel Title { get; set; }
        [JsonProperty("remark")]
        public WechatKeyWordModel Remark { get; set; }
    }
}