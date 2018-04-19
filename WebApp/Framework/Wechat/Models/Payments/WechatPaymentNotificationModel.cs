using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatPaymentNotificationModel
    {
        [XmlElement("out_trade_no")]
        public string TransactionId { get; set; }
        [XmlElement("total_fee")]
        public int Total { get; set; }
        public string Hash { get; set; }
        public bool IsValid()
        {
            return TransactionId != null;
        }
    }
}