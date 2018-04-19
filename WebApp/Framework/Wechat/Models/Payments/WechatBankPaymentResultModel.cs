using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatBankPaymentResultModel : WechatResultBaseModel
    {
        [XmlElement("payment_no")]
        public string WechatPaymentNumber { get; set; }
    }
}