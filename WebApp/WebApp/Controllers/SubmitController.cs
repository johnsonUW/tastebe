using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccess;
using Framework.Clover;
using Framework.ExceptionHandling;
using Microsoft.Ajax.Utilities;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/submit")]
    public class SubmitController : BaseController
    {
        [Route("order")]
        [HttpPost]
        public async Task<IHttpActionResult> SubmitOrder(OrderRequestModel model)
        {
            using (var context = new TasteContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == model.UserId);
                if (user == null) return Ok(GetErrorModel(ApiErrorCode.UserDoesNotExist));
                var restaurant = context.Restaurants.FirstOrDefault(r => r.Id == model.RestaurantId);
                if (restaurant == null) return Ok(GetErrorModel(ApiErrorCode.RestaurantDoesNotExist));


                string orderId;
                var order = context.Orders.FirstOrDefault(o =>
                    !o.Paid && o.RestaurantId == model.RestaurantId && o.UserId == model.UserId);
                if (order != null)
                {
                    orderId = order.OrderId;
                }
                else
                {
                    orderId = $"{user.UserId} {DateTime.Now}";
                    context.Orders.Add(new Order
                    {
                        Datetime = DateTime.Now,
                        OrderId = orderId,
                        Paid = false,
                        RestaurantId = model.RestaurantId,
                        TableName = model.Table.ToString(),
                        UserId = user.UserId,
                        Details = ""
                    });
                }

                var cloverLineItems = new List<CloverLineItemModel>();

                foreach (var i in model.Items)
                {
                    var dish = context.Dishes.FirstOrDefault(d => d.Id == i.DishId);
                    if (dish == null) continue;

                    cloverLineItems.Add(new CloverLineItemModel
                    {
                        PriceInPennies = Convert.ToInt32(dish.Price * 100),
                        Name = dish.Name,
                        Printed = true,
                        UnitQuantity = i.Quantity
                    });

                    context.OrderedDishes.Add(new OrderedDish
                    {
                        DishId = i.DishId,
                        Quantity = i.Quantity,
                        UserId = user.UserId,
                        OrderId = orderId,
                        CuisineId = dish.CuisineId.GetValueOrDefault()
                    });

                    var pref = context.Preferences.FirstOrDefault(p =>
                        p.UserId == user.UserId && p.CuisineId == dish.CuisineId);
                    if (pref != null) pref.Count += i.Quantity;
                    else
                    {
                        context.Preferences.Add(new Preference
                        {
                            Count = i.Quantity,
                            CuisineId = dish.CuisineId,
                            UserId = user.UserId
                        });
                    }
                }

                //send printing request to clover
                if (!restaurant.AccessToken.IsNullOrWhiteSpace())
                {
                    await CloverClient.CreateOrderAsync(cloverLineItems, restaurant.AccessToken, restaurant.CloverId,
                        restaurant.IsSandbox);
                }
                context.SaveChanges();
                return Ok(orderId);
            }
        }

        [Route("cashorcreditpay")]
        [HttpPost]
        public IHttpActionResult PayInCashOrCredit(PaymentRequestModel model)
        {
            using (var context = new TasteContext())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == model.OrderId);
                if (order != null)
                {
                    double total = 0;
                    var orderItems = context.OrderedDishes.Where(o => o.OrderId == model.OrderId && o.UserId == model.UserId);
                    foreach (var od in orderItems)
                    {
                        var item = context.Dishes.FirstOrDefault(it => it.Id == od.DishId);
                        if (item == null) continue;
                        total += item.Price * od.Quantity;
                    }

                    order.Paid = true;
                    order.TotalInPennies = Convert.ToInt32(total * 100);
                    order.TaxInPennies = Convert.ToInt32(order.TotalInPennies * 0.0925);
                    order.TipInPennies = 0;
                    context.SaveChanges();
                }
                return Ok();
            }
        }
    }
}
