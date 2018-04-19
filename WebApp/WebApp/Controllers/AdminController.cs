using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccess;
using Framework.Clover;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/admin")]
    public class AdminController : BaseController
    {
        [HttpPost]
        [Route("sync")]
        public async Task<IHttpActionResult> SyncWithClover(int restaurantId, string adminName, string adminPassword)
        {
            if (adminPassword != "password" || adminName != "taste") return Ok();
            using (var context = new Taste())
            {
                var restaurant = context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
                if (restaurant == null) return Ok();
                var result = await CloverClient.GetItemsAsync(restaurant.AccessToken, restaurant.CloverId, restaurant.IsSandbox);
                result.ForEach(r => context.Dishes.Add(new Dish
                {
                    Name = r.Name,
                    Price = r.Price / 100.0,
                    CuisineId = -1,
                    RestaurantId = restaurantId
                }));
                context.SaveChanges();
            }
            return Ok();
        }
    }
}
