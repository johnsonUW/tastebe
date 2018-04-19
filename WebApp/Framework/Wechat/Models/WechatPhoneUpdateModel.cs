namespace Framework.Wechat.Models
{
    public class WechatPhoneUpdateModel
    {
        public string EncryptedData { get; set; }
        public string Iv { get; set; }
        public string Code { get; set; }

        public bool IsValid()
        {
            return EncryptedData != null && Iv != null;
        }
    }
}