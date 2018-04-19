using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatPaymentVerificationResultModel : WechatResultBaseModel
    {
        [XmlElement("trade_state")]
        public string TradeState { get; set; }
        [XmlElement("out_trade_no")]
        public string TransactionId { get; set; }
        [XmlElement("transaction_id")]
        public string WechatTransactionId { get; set; }
    }
}