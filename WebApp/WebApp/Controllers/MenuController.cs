using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/menu")]
    public class MenuController : BaseController
    {
        [HttpGet]
        [Route("{restaurantId}")]
        public IHttpActionResult GetMenuByRestaurantId(int restaurantId)
        {
            using (var context = new Taste())
            {
                var menu = new List<MenuModel>();
                var cuisine = context.Cuisines.ToList();
                foreach (var c in cuisine)
                {
                    var dishes = context.Dishes.Where(d => d.CuisineId == c.Id && d.RestaurantId == restaurantId);
                    if (!dishes.Any()) continue;
                    var items = new List<DishModel>();
                    foreach (var d in dishes)
                    {
                        items.Add(ToModel(d));
                    }
                    menu.Add(new MenuModel
                    {
                        Cuisine = ToModel(c),
                        Items = items
                    });
                }
                return Ok(menu);
            }
        }
    }
}
