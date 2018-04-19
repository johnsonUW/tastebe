using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatOrderPickedupTemplateDataModel : WechatTemplateDataModel
    {
        [JsonProperty("keyword1")]
        public WechatKeyWordModel OrderNumber { get; set; }
        [JsonProperty("keyword2")]
        public WechatKeyWordModel OrderHeader { get; set; }
        [JsonProperty("keyword3")]
        public WechatKeyWordModel TechnicianName { get; set; }
        [JsonProperty("keyword4")]
        public WechatKeyWordModel ContactNumber { get; set; }
    }
}