using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccess;
using Framework.Clover;
using WebGrease.Css.Extensions;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/admin")]
    public class AdminController : BaseController
    {
        [HttpGet]
        [Route("sync")]
        public async Task<IHttpActionResult> SyncWithClover()
        {
            using (var context = new TasteContext())
            {
                var restaurants = context.Restaurants.ToList();
                foreach(var restaurant in restaurants)
                {
                    var restaurantId = restaurant.Id;
                    var result = await CloverClient.GetItemsAsync(restaurant.AccessToken, restaurant.CloverId, restaurant.IsSandbox);

                    var existingMenu = context.Dishes.Where(d => !d.Deleted && d.RestaurantId == restaurantId).ToList();
                    var ids = existingMenu.Select(d => d.CloverId).ToList();
                    ids.RemoveAll(s => s == null);

                    var newMenu = result.Select(r => r.Id).ToList();

                    var itemsToBeDeleted = ids.Where(m => !newMenu.Contains(m)).ToList();
                    var itemsToBeAdded = newMenu.Where(m => !ids.Contains(m)).ToList();

                    foreach (var i in itemsToBeAdded)
                    {
                        var r = result.First(re => re.Id == i);
                        context.Dishes.Add(new Dish
                        {
                            Name = r.Name,
                            Price = r.Price / 100.0,
                            CuisineId = -1,
                            RestaurantId = restaurantId,
                            CloverId = r.Id,
                            Deleted = false
                        });
                    }

                    foreach (var i in itemsToBeDeleted)
                    {
                        var r = existingMenu.First(re => re.CloverId == i);
                        context.Dishes.Remove(r);
                    }

                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
