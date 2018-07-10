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
    [RoutePrefix("api/v1/dish")]
    public class DishController : BaseController
    {
        [HttpPost]
        [Route("editDish")]
        public IHttpActionResult EditDishInformation([FromBody] Dish dish)
        {
            using (var context = new TasteContext())
            {
                var retrievedDish = context.Dishes.FirstOrDefault(d => d.Id == dish.Id);
                if (retrievedDish == null) return Ok();
                //retrievedDish.RestaurantId = dish.RestaurantId;
                retrievedDish.CuisineId = dish.CuisineId;
                //retrievedDish.Name = dish.Name;
                retrievedDish.Description = dish.Description;
                retrievedDish.Flavors = dish.Flavors;
                retrievedDish.Ingredients = dish.Ingredients;
                retrievedDish.Category = dish.Category;
                //retrievedDish.Price = dish.Price;
                retrievedDish.Image = dish.Image;
                //retrievedDish.Deleted = dish.Deleted;
                //retrievedDish.CloverId = dish.CloverId;
                
                context.SaveChanges();

                return Ok("Save success");
            }
        }

        [HttpPost]
        [Route("addDish")]
        public IHttpActionResult AddDishInformation([FromBody] Dish dish)
        {
            using (var context = new TasteContext())
            {
                if (dish == null) return Ok("dish is null");

                Dish d = new Dish
                {
                    RestaurantId = dish.RestaurantId,
                    CuisineId = dish.CuisineId,
                    Name = dish.Name,
                    Description = dish.Description,
                    Flavors = dish.Flavors,
                    Ingredients = dish.Ingredients,
                    Category = dish.Category,
                    Price = dish.Price,
                    Image = dish.Image,
                    Deleted = false
                };

                context.SaveChanges();

                return Ok("Save success");
            }
        }

        [HttpGet]
        [Route("cuisinecategory")]
        public IHttpActionResult GetAllCategory()
        {
            using (var context = new TasteContext())
            {
                return Ok(context.Cuisine.ToList());
            }
        }

    }
}
