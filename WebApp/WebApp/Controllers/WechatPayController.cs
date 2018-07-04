using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using DataAccess;
using Framework.Cryptography;
using Framework.ExceptionHandling;
using Framework.Models;
using Framework.Utils;
using Framework.Wechat;
using Framework.Wechat.Models.Enums;
using Framework.Wechat.Models.Payments;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/payment")]
    public class WechatPayController : BaseController
    {
        [Route("initialize")]
        [HttpPost]
        public async Task<IHttpActionResult> InitializePayment(PaymentRequestModel model)
        {
            using (var context = new TasteContext())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == model.OrderId && o.UserId == model.UserId);
                if (order == null || order.Paid) return Ok(GetErrorModel(ApiErrorCode.OrderDoesNotExist));
                var restaurant = context.Restaurants.FirstOrDefault(r => r.Id == order.RestaurantId);
                if (restaurant == null) return Ok(GetErrorModel(ApiErrorCode.RestaurantDoesNotExist));

                const string body = "豪吃";
                const string signType = "MD5";
                var appId = ConfigurationManager.AppSettings.Get("WeChatMiniProgramAppId");
                var merchantId = ConfigurationManager.AppSettings.Get("WeChatMerchantId");
                var merchantKey = ConfigurationManager.AppSettings.Get("WeChatMerchantKey");
                var notifyUrl = $"{RequestBaseUrl}/api/v1/payment/complete";
                var userAddress = HttpContext.Current.Request.UserHostAddress;
                var nonce = Guid.NewGuid().ToString().Replace("-", "");
                var timeStamp = DateTime.Now.SecondsSinceEpoch().ToString();
                var transactionId = Guid.NewGuid().ToString().Replace("-", "");

                double total = 0;
                var orderItems = context.OrderedDishes.Where(o => o.OrderId == model.OrderId && o.UserId == model.UserId);
                foreach (var od in orderItems)
                {
                    var item = context.Dishes.FirstOrDefault(it => it.Id == od.DishId);
                    if (item == null) continue;
                    total += item.Price * od.Quantity;
                }


                context.Payments.Add(new Payment
                {
                    Success = false,
                    TransactionId = transactionId,
                    CreationDate = DateTimeExtensions.BeiJingNow(),
                    UserId = model.UserId,
                    OrderId = model.OrderId
                });
                order.TipInPennies = model.TipInPennies;
                order.TotalInPennies = Convert.ToInt32(total * 100);
                order.TaxInPennies = Convert.ToInt32(order.TotalInPennies * 0.0925);
                context.SaveChanges();

                var subTotalRmb = Convert.ToInt32(order.TotalInPennies * restaurant.ExchangeRate);
                var tipRmb = Convert.ToInt32(model.TipInPennies * restaurant.ExchangeRate);
                var taxInPenniesRmb = Convert.ToInt32(order.TaxInPennies * restaurant.ExchangeRate);
                var orderTotalRmb = subTotalRmb + tipRmb + taxInPenniesRmb;

                var result = await WechatPayHttpClient.GetPaymentInfo(userAddress, notifyUrl, appId, orderTotalRmb, WechatTradeType.JSAPI, transactionId, body, merchantId, model.UserId, merchantKey);
                var paymentData = new WechatPaymentModel
                {
                    Body = body,
                    NotifyUrl = notifyUrl,
                    TransactionId = transactionId,
                    TotalAmountInPennies = orderTotalRmb,
                    PrepayId = result.PrepayId,
                    PaySign = WechatMd5SignGenerator.GetPaymentSignMd5Hash(appId, timeStamp, nonce, result.PrepayId, signType, merchantKey),
                    Nonce = nonce,
                    SignType = signType,
                    TimeStamp = timeStamp,
                    WebUrl = result.WebUrl
                };

                return Ok(paymentData);
            }
        }

        [Route("complete")]
        [HttpPost]
        [HttpPut]
        public async Task<IHttpActionResult> CompletePayment(WechatPaymentNotificationModel notification)
        {
            if (notification == null || !notification.IsValid()) return Ok(GetErrorModel(ApiErrorCode.InvalidData));
            using (var context = new TasteContext())
            {
                var payment = context.Payments.FirstOrDefault(o => o.TransactionId == notification.TransactionId);
                if (payment == null || payment.Success) return Ok();
                var order = context.Orders.FirstOrDefault(o =>
                    o.OrderId == payment.OrderId && o.UserId == payment.UserId);
                if (order == null) return Ok();
                var restaurant = context.Restaurants.FirstOrDefault(r => r.Id == order.RestaurantId);
                if (restaurant == null) return Ok();

                var merchantId = ConfigurationManager.AppSettings.Get("WeChatMerchantId");
                var merchantKey = ConfigurationManager.AppSettings.Get("WeChatMerchantKey");

                var paymentMade = await WechatPayHttpClient.CheckIfPaymentIsPaidAsync(payment.TransactionId, ConfigurationManager.AppSettings.Get("WeChatMiniProgramAppId"), merchantId, merchantKey);
                if (!paymentMade) return Ok(GetErrorModel(ApiErrorCode.PaymentNotMade));

                payment.Success = true;
                order.Paid = true;
                restaurant.OustandingBalance += Convert.ToInt32(notification.Total / restaurant.ExchangeRate);
                context.SaveChanges();

                return Ok();
            }
        }
    }
}
