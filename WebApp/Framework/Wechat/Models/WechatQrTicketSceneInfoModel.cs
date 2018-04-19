using Newtonsoft.Json;

namespace Framework.Wechat.Models
{
    public class WechatQrTicketSceneInfoModel
    {
        [JsonProperty("scene_id")]
        public int SceneId { get; set; }
    }
}