using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatPublicKeyResponseModel : WechatResultBaseModel
    {
        [XmlElement("pub_key")]
        public string Key { get; set; }
    }
}