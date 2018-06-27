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
