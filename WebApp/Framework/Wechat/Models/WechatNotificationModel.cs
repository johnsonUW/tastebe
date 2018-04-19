using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatNotificationModel<T> where T : WechatTemplateDataModel
    {
        [JsonProperty("touser")]
        public string RecipientWechatId { get; set; }
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }
        [JsonProperty("miniprogram")]
        public WechatMiniProgramModel MiniProgram { get; set; }
        [JsonProperty("data")]
        public T TemplateData { get; set; }
    }
}