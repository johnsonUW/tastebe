using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DataAccess;
using Framework.ExceptionHandling;
using WebApp.Models;


namespace WebApp.Controllers
{
    [RoutePrefix("api/v1/restaurants")]
    public class RestaurantController : BaseController
    {
        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllRestaurants()
        {
            using (var context = new TasteContext())
            {
                var list = new List<RestaurantModel>();
                foreach (var r in context.Restaurants)
                {
                    list.Add(ToModel(r));
                }
                return Ok(list);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetRestaurantById(int id)
        {
            using (var context = new TasteContext())
            {
                var res = context.Restaurants.FirstOrDefault(r => r.Id == id);
                if (res != null) return Ok(ToModel(res));
                return Ok(GetErrorModel(ApiErrorCode.RestaurantDoesNotExist));
            }
        }

        [Route("user/{username}")]
        [HttpGet]
        public IHttpActionResult GetRestaurantByUserId(string username)
        {
            using (var context = new TasteContext())
            {
                var admin = context.Admins.FirstOrDefault(a => a.Username == username);
                if (admin != null)
                {
                    var res = context.Restaurants.FirstOrDefault(r => r.Name.Equals(admin.RestaurantName));

                    if (res != null)
                    {
                        return Ok(ToModel(res));
                    }
                    else
                    {
                        return Ok(GetErrorModel(ApiErrorCode.RestaurantDoesNotExist));
                    }
                }
                else
                {
                    return Ok(GetErrorModel(ApiErrorCode.RestaurantDoesNotExist));
                }
            }
        }

        [Route("edit")]
        [HttpPost]
        public IHttpActionResult EditRestaurantInfo([FromBody] Restaurant restaurant)
        {
            using (var context = new TasteContext())
            {
                var res = context.Restaurants.FirstOrDefault(r => r.CloverId == restaurant.CloverId);
                if (res != null)
                {
                    res.Location = restaurant.Location;
                    res.Name = restaurant.Name;
                    res.Owner = restaurant.Owner;
                    res.Phone = restaurant.Phone;

                    context.SaveChanges();

                    return Json("Success");
                }
                else
                {
                    return Ok(GetErrorModel(ApiErrorCode.RestaurantDoesNotExist));
                }
            }
        }
    }
}