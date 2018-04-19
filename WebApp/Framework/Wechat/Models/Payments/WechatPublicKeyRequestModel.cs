using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatPublicKeyRequestModel
    {
        [XmlElement("mch_id")]
        public string MerchantId { get; set; }
        [XmlElement("nonce_str")]
        public string Nonce { get; set; }
        [XmlElement("sign")]
        public string Sign { get; set; }
        [XmlElement("sign_type")]
        public string SignType { get; set; }
    }
}