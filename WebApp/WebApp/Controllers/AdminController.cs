using System;
using System.Linq;
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
        [Route("checkIfCloverUserExists")]
        public IHttpActionResult CheckIfCloverUserExists([FromBody]Signup signup)
        {
            using (var context = new TasteContext())
            {
                if (context.Admins.Where(a => a.Username.Equals(signup.username)).ToList().Count() > 0)
                {
                    return Json("Exist");
                } 
                else
                {
                    return Json("Doesn't exist");
                }
            }
        }

        [HttpPost]
        [Route("createCloverLogin")]
        public IHttpActionResult CreateCloverLogin([FromBody]Signup signup)
        {
            using (var context = new TasteContext())
            {
                if (context.Admins.Where(a => a.Username.Equals(signup.username)).ToList().Count() > 0)
                {
                    return Unauthorized();
                }

                context.Admins.Add(new Admins()
                {
                    Username = signup.username,
                    Password = signup.password,
                    RestaurantName = signup.username
                });

                var res = context.Restaurants.ToList();
                context.Restaurants.Add(new Restaurant()
                {
                    CloverId = signup.username,
                    Name = signup.username,
                    Location = "",
                    Phone = "",
                    Owner = "",
                    Image = "",
                    AccessToken = signup.password,
                    ExchangeRate = 6.5,
                    IsSandbox = false
                });
                context.SaveChanges();
            }

            return Json("Success");
        }

        [HttpPost]
        [Route("createLogin")]
        public IHttpActionResult CreateLogin([FromBody]Signup signup)
        {
            Console.Write(signup);
            using (var context = new TasteContext())
            {
                if (context.Admins.Where(a => a.Username.Equals(signup.username)).ToList().Count() > 0)
                {
                    return Unauthorized();
                }

                context.Admins.Add(new Admins()
                {
                    Username = signup.username,
                    Password = signup.password,
                    RestaurantName = signup.name
                });

                var res = context.Restaurants.ToList();
                context.Restaurants.Add(new Restaurant()
                {
                    CloverId = signup.cloverId,
                    Name = signup.name,
                    Location = signup.location,
                    Phone = signup.phone,
                    Owner = signup.owner,
                    Image = "",
                    AccessToken = signup.accessToken,
                    ExchangeRate = 6.5,
                    IsSandbox = false
                });
                context.SaveChanges();
            }

            return Json("Success");
        }


        [HttpGet]
        [Route("outstandingBalance")]
        public IHttpActionResult PayOutstandingBalance(int restaurantId, int amount)
        {
            using (var context = new TasteContext())
            {
                var restaurant = context.Restaurants.FirstOrDefault(r => r.Id == restaurantId);
                if (restaurant == null)
                {
                    return NotFound();
                }

                restaurant.OustandingBalance -= amount;
                context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet]
        [Route("login")]
        public IHttpActionResult LoginAccount(string username, string password, string fromClover = "false")
        {
            using (var context = new TasteContext())
            {
                var user = context.Admins.FirstOrDefault(r => r.Username == username);
                if (user == null)
                {
                    return Unauthorized();
                }

                if (fromClover.Equals("false"))
                {
                    // check password, else just let it go
                    if (password.Equals(user.Password))
                    {
                        return Ok("Success");
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    // update password and restaurant's accesstoken
                    user.Password = password;
                    var res = context.Restaurants.FirstOrDefault(r => r.CloverId.Equals(username));
                    res.AccessToken = password;
                    context.SaveChanges();

                    return Ok("Success");
                }
            }
        }

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

                    if (result != null)
                    {
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
                    else
                    {
                        // nothing
                    }
                }
            }
            return Ok("Success");
        }
    }
}
