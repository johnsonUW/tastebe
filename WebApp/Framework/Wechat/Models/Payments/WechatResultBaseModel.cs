using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    public class WechatResultBaseModel
    {
        [XmlElement("return_code")]
        public string ReturnCode { get; set; }
        [XmlElement("result_code")]
        public string ResultCode { get; set; }
        [XmlElement("err_code")]
        public string ErrorCode { get; set; }
        [XmlElement("err_code_des")]
        public string ErrorDescription { get; set; }
    }
}