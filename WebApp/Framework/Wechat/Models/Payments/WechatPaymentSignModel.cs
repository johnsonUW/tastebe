namespace Framework.Wechat.Models.Payments
{
    public class WechatPaymentSignModel
    {
        public string AppId { get; set; }
        public string TimeStamp { get; set; }
        public string Nonce { get; set; }
        public string Package { get; set; }
    }
}