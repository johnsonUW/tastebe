using Framework.Cryptography;

namespace Framework.Wechat
{
    public static class WechatMd5SignGenerator
    {
        public static string GetMd5Hash(string appId, string merchantId, string nonce, string body, string transactionId, int totalFeeInPennies, string ip, string notifyUrl, string tradeType, string openId, string key)
        {
            var concatenationString =
                $"appid={appId}&body={body}&mch_id={merchantId}&nonce_str={nonce}&notify_url={notifyUrl}&openid={openId}&out_trade_no={transactionId}&spbill_create_ip={ip}&total_fee={totalFeeInPennies}&trade_type={tradeType}&key={key}";
            return Md5Hash.Hash(concatenationString).ToUpper();
        }

        public static string GetMd5Hash(string appId, string merchantId, string nonce, string transactionId, string key)
        {
            var concatenationString =
                $"appid={appId}&mch_id={merchantId}&nonce_str={nonce}&out_trade_no={transactionId}&key={key}";
            return Md5Hash.Hash(concatenationString).ToUpper();
        }

        public static string GetPaymentSignMd5Hash(string appId, string timeStamp, string nonce, string package, string signType, string key)
        {
            var concatenationString =
                $"appId={appId}&nonceStr={nonce}&package=prepay_id={package}&signType={signType}&timeStamp={timeStamp}&key={key}";
            return Md5Hash.Hash(concatenationString).ToUpper();
        }

        public static string GetMd5Hash(string appId, string merchantId, string nonce, string transactionId, string refundId, string totalFeeInPennies, string refundFeeInPennies, string key)
        {
            var concatenationString =
                $"appid={appId}&mch_id={merchantId}&nonce_str={nonce}&op_user_id={merchantId}&out_trade_no={transactionId}&out_refund_no={refundId}&refund_fee={refundFeeInPennies}&total_fee={totalFeeInPennies}&key={key}";
            return Md5Hash.Hash(concatenationString).ToUpper();
        }

        public static string GetMd5Hash(string merchantId, string nonce, string signType, string key)
        {
            var concatenationString =
                $"mch_id={merchantId}&nonce_str={nonce}&sign_type={signType}&key={key}";
            return Md5Hash.Hash(concatenationString).ToUpper();
        }

        public static string GetMd5Hash(string merchantId, string nonce, string paymentNumber, string encryptedCardNumber, string encryptedRecipientName, string bankCode, int paymentAmount, string paymentDescription, string key)
        {
            var concatenationString =
                $"amount={paymentAmount}&bank_code={bankCode}&desc={paymentDescription}&enc_bank_no={encryptedCardNumber}&enc_true_name={encryptedRecipientName}&mch_id={merchantId}&nonce_str={nonce}&partner_trade_no={paymentNumber}&key={key}";
            return Md5Hash.Hash(concatenationString).ToUpper();
        }

        public static string GetMd5Hash(string merchantId, string nonce, string paymentNumber, string appId, string userOpenId, string checkName, int paymentAmount, string paymentDescription, string requestIpAddress, string key)
        {
            var concatenationString =
                $"amount={paymentAmount}&check_name={checkName}&desc={paymentDescription}&mch_appid={appId}&mchid={merchantId}&nonce_str={nonce}&openid={userOpenId}&partner_trade_no={paymentNumber}&spbill_create_ip={requestIpAddress}&key={key}";
            return Md5Hash.Hash(concatenationString).ToUpper();
        }

        public static string GetPaymentInfoMd5Hash(string merchantId, string publicAccountId, string nonce, string description, string transactionId, int totalFee, string userIp, string notifyUrl, string tradeType, string openid, string key)
        {
            var concatenationString =
                $"appid={publicAccountId}&body={description}&mch_id={merchantId}&nonce_str={nonce}&notify_url={notifyUrl}&openid={openid}&out_trade_no={transactionId}&spbill_create_ip={userIp}&total_fee={totalFee}&trade_type={tradeType}&key={key}";
            return Md5Hash.Hash(concatenationString).ToUpper();
        }
    }
}