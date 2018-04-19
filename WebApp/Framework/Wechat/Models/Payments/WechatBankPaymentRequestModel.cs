using System.Xml.Serialization;

namespace Framework.Wechat.Models.Payments
{
    [XmlType(TypeName = "xml")]
    public class WechatBankPaymentRequestModel
    {
        [XmlElement("mch_id")]
        public string MerchantId { get; set; }
        [XmlElement("partner_trade_no")]
        public string PaymentNumber { get; set; }
        [XmlElement("nonce_str")]
        public string Nonce { get; set; }
        [XmlElement("sign")]
        public string Sign { get; set; }
        [XmlElement("enc_bank_no")]
        public string EncryptedCardNumber { get; set; }
        [XmlElement("enc_true_name")]
        public string EncryptedRecipientName { get; set; }
        [XmlElement("bank_code")]
        public string BankCode { get; set; }
        [XmlElement("amount")]
        public int PaymentAmountInPennies { get; set; }
        [XmlElement("desc")]
        public string PaymentDescription { get; set; }
    }
}