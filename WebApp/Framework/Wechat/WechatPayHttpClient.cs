using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using Framework.Cryptography;
using Framework.ExceptionHandling;
using Framework.Logging;
using Framework.Models;
using Framework.Utils;
using Framework.Wechat.Models;
using Framework.Wechat.Models.Enums;
using Framework.Wechat.Models.Payments;

namespace Framework.Wechat
{
    public static class WechatPayHttpClient
    {
        /// <summary>
        /// merchant id is required when certificate is needed
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="needCertificate"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        private static HttpClient GetClient(string baseAddress, bool needCertificate = false, string merchantId = "")
        {
            if (!needCertificate)
                return new HttpClient
                {
                    BaseAddress = new Uri(baseAddress)
                };
            if (needCertificate && merchantId == "") throw new ApiException(ApiErrorCode.InvalidData);
            var handler = new WebRequestHandler();
            var serverPath = HttpContext.Current == null ? "" : $"{HttpContext.Current.Server.MapPath("/")}/";
            handler.ClientCertificates.Add(new X509Certificate2($"{serverPath}apiclient_cert.pfx", merchantId));
            return new HttpClient(handler)
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public static async Task<bool> CheckIfPaymentIsPaidAsync(string transactionId, string appId, string merchantId, string merchantKey)
        {
            var nonce = Guid.NewGuid().ToString().Replace("-", "");
            var sign = WechatMd5SignGenerator.GetMd5Hash(appId, merchantId, nonce, transactionId, merchantKey);

            var verification = new WechatPaymentVerificationModel
            {
                AppId = appId,
                MerchantId = merchantId,
                Nonce = nonce,
                Sign = sign,
                TransactionId = transactionId
            };

            var response = await PostAsync<WechatPaymentVerificationModel, WechatPaymentVerificationResultModel>("pay/orderquery", verification);

            var paid = string.Equals(response.ResultCode, WechatPayStatus.SUCCESS.ToString()) && string.Equals(response.ReturnCode, WechatPayStatus.SUCCESS.ToString()) && string.Equals(response.TradeState, WechatTradeState.SUCCESS.ToString()) && string.Equals(transactionId, response.TransactionId);
            if (!paid) LoggingQueue.Add(new LogMessage
            {
                LogLevel = LogLevel.Log,
                Message = $"Wechat paid status: TransactionId: {transactionId}, ReturnCode: {response.ReturnCode}, ResultCode: {response.ResultCode}"
            });
            return paid;
        }

        public static async Task<WechatPaymentDataResultModel> GetPaymentInfo(string appIp, string notifyUrl, string publicAccountAppId, int feeInPennies, WechatTradeType tradeType, string transactionId, string desc, string merchantId, string openId, string merchantKey)
        {
            var nonce = Guid.NewGuid().ToString().Replace("-", "");
            var sign = WechatMd5SignGenerator.GetPaymentInfoMd5Hash(merchantId, publicAccountAppId, nonce, desc, transactionId, feeInPennies, appIp, notifyUrl, tradeType.ToString(), openId, merchantKey);

            var paymentRequestModel = new WechatPaymentRequestModel
            {
                AppIp = appIp,
                Description = desc,
                MerchantId = merchantId,
                Nonce = nonce,
                NotifyUrl = notifyUrl,
                PublicAccountAppId = publicAccountAppId,
                TotalFee = feeInPennies,
                TradeType = tradeType.ToString(),
                TransactionId = transactionId,
                Sign = sign,
                UserOpenId = openId
            };

            var response = await PostAsync<WechatPaymentRequestModel, WechatPaymentDataResultModel>("pay/unifiedorder", paymentRequestModel);

            var success = string.Equals(response.ResultCode, WechatPayStatus.SUCCESS.ToString()) && string.Equals(response.ReturnCode, WechatPayStatus.SUCCESS.ToString());
            if (!success)
            {
                LoggingQueue.Add(new LogMessage
                {
                    LogLevel = LogLevel.Log,
                    Message = $"Wechat refund status: TransactionId: {transactionId}, ReturnCode: {response.ReturnCode}, ResultCode: {response.ResultCode}"
                });
            }
            return response;
        }


        /// <summary>
        /// merchantid is required when certificate is needed
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="needCertificate"></param>
        /// <param name="merchantId"></param>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        private static async Task<TOut> PostAsync<TIn, TOut>(string url, TIn data, bool needCertificate = false, string merchantId = "", string baseAddress = "https://api.mch.weixin.qq.com") where TOut : WechatResultBaseModel
        {
            var client = GetClient(baseAddress, needCertificate, merchantId);
            var result = await client.PostAsync(url, new StringContent(data.ToXml()));
            if (!result.IsSuccessStatusCode || result.Content == null) throw new ApiException(ApiErrorCode.WechatPaymentError, result.Content?.ReadAsStringAsync().Result);
            var x = result.Content.ReadAsStringAsync();
            var response = (await result.Content.ReadAsStringAsync()).FromXml<TOut>();
            if (response == null) throw new ApiException(ApiErrorCode.WechatPaymentError, "Payment status API serilization failed");
            return response;
        }
    }
}