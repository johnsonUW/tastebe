using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatPaymentVerificationModel
    {
        [XmlElement("appid")]
        public string AppId { get; set; }
        [XmlElement("mch_id")]
        public string MerchantId { get; set; }
        [XmlElement("nonce_str")]
        public string Nonce { get; set; }
        [XmlElement("out_trade_no")]
        public string TransactionId { get; set; }
        [XmlElement("sign")]
        public string Sign { get; set; }
    }
}