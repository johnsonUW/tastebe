using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatRefundVerificationModel
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
        [XmlElement("out_refund_no")]
        public string RefundId { get; set; }
        [XmlElement("refund_fee")]
        public string RefundAmountInPennies { get; set; }
        [XmlElement("total_fee")]
        public string OrderAmountInPennies { get; set; }
        [XmlElement("op_user_id")]
        public string MerchantIdentifier { get; set; }
    }
}