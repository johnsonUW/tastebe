using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatPaymentDataResultModel : WechatResultBaseModel
    {
        [XmlElement("prepay_id")]
        public string PrepayId { get; set; }
        [XmlElement("trade_type")]
        public string TradeType { get; set; }
        [XmlElement("mweb_url")]
        public string WebUrl { get; set; }
    }
}