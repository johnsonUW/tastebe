using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatPaymentNotificationResponseModel : WechatResultBaseModel

    {
        [XmlElement("return_msg")]
        public string ReturnMessage { get; set; }
    }
}