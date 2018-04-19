using System.Xml.Serialization;

namespace Framework.Wechat.Models
{
    [XmlType(TypeName = "xml")]
    public class WechatPushModel
    {
        [XmlElement("ToUserName")]
        public string ToUserName { get; set; }
        [XmlElement("FromUserName")]
        public string FromUserName { get; set; }
        [XmlElement("CreateTime")]
        public string CreateTime { get; set; }
        [XmlElement("MsgType")]
        public string MessageType { get; set; }
        [XmlElement("Event")]
        public string Event { get; set; }
        [XmlElement("EventKey")]
        public string EventKey { get; set; }
        [XmlElement("Ticket")]
        public string Ticket { get; set; }
    }
}