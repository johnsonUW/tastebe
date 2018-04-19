using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatPaymentRequestModel
    {
        [XmlElement("appid")]
        public string PublicAccountAppId { get; set; }
        [XmlElement("mch_id")]
        public string MerchantId { get; set; }
        [XmlElement("nonce_str")]
        public string Nonce { get; set; }
        [XmlElement("out_trade_no")]
        public string TransactionId { get; set; }
        [XmlElement("sign")]
        public string Sign { get; set; }
        [XmlElement("body")]
        public string Description { get; set; }
        [XmlElement("total_fee")]
        public int TotalFee { get; set; }
        [XmlElement("spbill_create_ip")]
        public string AppIp { get; set; }
        [XmlElement("notify_url")]
        public string NotifyUrl { get; set; }
        [XmlElement("trade_type")]
        public string TradeType { get; set; }
        [XmlElement("openid")]
        public string UserOpenId { get; set; }
        [XmlElement("scene_info")]
        public string SceneInfo { get; set; }

    }
}