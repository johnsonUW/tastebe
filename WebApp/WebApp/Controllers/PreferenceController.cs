using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using DataAccess;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/preferences")]
    public class PreferenceController : BaseController
    {
        [Route("{userId}")]
        [HttpGet]
        public IHttpActionResult GetUserPreferences(string userId, string restaurantId)
        {
            using (var context = new Taste())
            {
                var pref = context.Preferences.Where(p => p.UserId == userId).OrderByDescending(p => p.Count).ToList();
                if (!pref.Any()) return Ok();

                var dishes = new List<DishModel>();
                foreach (var p in pref)
                {
                    var newDishes = context.Dishes.Where(d => d.Cuisine.Id == p.CuisineId);
                    foreach (var d in newDishes)
                    {
                        dishes.Add(ToModel(d));
                    }
                    //try not to show too many items in preference section
                    if (dishes.Count > 10) break;
                }
                return Ok(dishes);
            }
        }
    }
}
