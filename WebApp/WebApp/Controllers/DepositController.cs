using System;
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
    [RoutePrefix("api/v1/deposit")]
    public class DepositController : BaseController
    {
        [HttpGet]
        [Route("{restaurantId}")]
        public IHttpActionResult GetMoneyOwningRestaurant(int restaurantId)
        {
            using (var context = new TasteContext())
            {
                var orders = context.Orders.Where(o => o.RestaurantId == restaurantId).ToList();
                var sumTotal = 0;
                foreach (var o in orders)
                {
                    var orderTotal = o.TotalInPennies + o.TaxInPennies + o.TipInPennies;
                    sumTotal += orderTotal;
                }
                return Ok(sumTotal);
            }
        }

        [HttpGet]
        [Route("tips/{restaurantId}")]
        public IHttpActionResult GetTipsForRestaurant(int restaurantId)
        {
            using (var context = new TasteContext())
            {
                var orders = context.Orders.Where(o => o.RestaurantId == restaurantId).ToList();
                var sumTotal = 0;
                foreach (var o in orders)
                {
                    var orderTotal = o.TipInPennies;
                    sumTotal += orderTotal;
                }
                return Ok(sumTotal);
            }
        }

        [HttpPost]
        [Route("paytips/{restaurantId}")]
        public IHttpActionResult PayTips(int restaurantId)
        {
            using (var context = new TasteContext())
            {
                var orders = context.Orders.Where(o => o.RestaurantId == restaurantId).ToList();
                foreach (var o in orders)
                {
                    o.TipInPennies = 0;
                }
                context.SaveChanges();
            }

            return Json("Success");
        }

        [HttpPost]
        [Route("submit")]
        public IHttpActionResult AddDeposit([FromBody]Deposits deposits)
        {
            using (var context = new TasteContext())
            {
                context.Deposits.Add(deposits);

                context.SaveChanges();

                return Ok("Save success");
            }
        }
    }
}
