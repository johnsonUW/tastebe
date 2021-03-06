﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using Framework.ExceptionHandling;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/orders")]
    public class OrderController : BaseController
    {
        [HttpDelete]
        [Route]
        public IHttpActionResult DeleteOrder(Order or)
        {
            using (var context = new TasteContext())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == or.OrderId);
                var items = context.OrderedDishes.Where(od => od.OrderId == or.OrderId).ToList();
                if (order != null && !order.Paid)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    context.OrderedDishes.RemoveRange(items);
                    context.SaveChanges();
                }
                return Ok();
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public IHttpActionResult GetOrderHistoryById(string userId, int restaurantId)
        {
            using (var context = new TasteContext())
            {
                var list = new List<OrderHistoryModel>();
                var orders = context.Orders.Where(o => o.UserId == userId && o.RestaurantId == restaurantId).ToList();

                foreach (var o in orders)
                {
                    var details = context.OrderedDishes.Where(od => od.OrderId == o.OrderId);
                    var models = new List<OrderedDishModel>();
                    foreach (var d in details)
                    {
                        models.Add(ToModel(d));
                    }
                    list.Add(new OrderHistoryModel
                    {
                        DateTime = o.Datetime,
                        OrderId = o.OrderId,
                        Table = o.TableName,
                        Details = models,
                        TipInPennies = o.Paid ? o.TipInPennies : 0,
                        TotalInPennies = o.TotalInPennies,
                        TaxInPennies = o.TaxInPennies
                    });
                }
                return Ok(list);
            }
        }

        [HttpPost]
        [Route]
        public IHttpActionResult GetOrderHistoryByOrderId(OrderHistoryModel model)
        {
            using (var context = new TasteContext())
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == model.OrderId);
                if (order == null) return Ok(GetErrorModel(ApiErrorCode.OrderDoesNotExist));

                var details = context.OrderedDishes.Where(od => od.OrderId == order.OrderId);
                var models = new List<OrderedDishModel>();
                foreach (var d in details)
                {
                    models.Add(ToModel(d));
                }
                return Ok(new OrderHistoryModel
                {
                    DateTime = order.Datetime,
                    OrderId = order.OrderId,
                    Table = order.TableName,
                    Details = models,
                    TipInPennies = order.Paid ? order.TipInPennies : 0,
                    TotalInPennies = order.TotalInPennies,
                    TaxInPennies = order.TaxInPennies
                });
            }
        }
    }
}
