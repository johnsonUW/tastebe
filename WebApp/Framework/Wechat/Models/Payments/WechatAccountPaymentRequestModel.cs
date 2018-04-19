using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatAccountPaymentRequestModel
    {
        [XmlElement("mchid")]
        public string MerchantId { get; set; }
        [XmlElement("mch_appid")]
        public string WechatAppId { get; set; }
        [XmlElement("partner_trade_no")]
        public string PaymentNumber { get; set; }
        [XmlElement("nonce_str")]
        public string Nonce { get; set; }
        [XmlElement("sign")]
        public string Sign { get; set; }
        [XmlElement("openid")]
        public string WechatUserOpenId { get; set; }
        [XmlElement("check_name")]
        public string CheckName { get; set; }
        [XmlElement("spbill_create_ip")]
        public string RequestIpAddress { get; set; }
        [XmlElement("amount")]
        public int PaymentAmountInPennies { get; set; }
        [XmlElement("desc")]
        public string PaymentDescription { get; set; }
    }
}