using Framework.Models;

namespace Framework.Wechat.Models
{
    public class WechatLoginModel
    {
        public string Code { get; set; }
        public bool IsValid()
        {
            return Code != null;
        }
    }
}