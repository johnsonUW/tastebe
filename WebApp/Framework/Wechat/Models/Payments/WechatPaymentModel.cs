namespace Framework.Wechat.Models.Payments
{
    public class WechatPaymentModel
    {
        public string Body { get; set; }
        public string TransactionId { get; set; }
        public string NotifyUrl { get; set; }
        public int TotalAmountInPennies { get; set; }
        public string PrepayId { get; set; }
        public string WebUrl { get; set; }
        public string PaySign { get; set; }
        public string TimeStamp { get; set; }
        public string Nonce { get; set; }
        public string SignType { get; set; }
    }
}