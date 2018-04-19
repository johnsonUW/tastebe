using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using Framework.ExceptionHandling;
using Framework.Models;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : ApiController
    {

        internal static ErrorModel GetErrorModel(ApiErrorCode errorCode)
        {
            return new ErrorModel
            {
                ErrorCode = errorCode,
                Message = errorCode.ToString()
            };
        }

        private string _requestBaseUrl;

        protected string RequestBaseUrl => _requestBaseUrl ??
                                           (_requestBaseUrl =
                                               Request.RequestUri.Scheme + "://" + Request.RequestUri.Host);

        internal static CuisineModel ToModel(Cuisine c)
        {
            return new CuisineModel
            {
                Id = c.Id,
                Name = c.Name
            };
        }

        internal static DishModel ToModel(Dish c)
        {
            return new DishModel
            {
                Category = c.Category,
                CuisineId = c.CuisineId.GetValueOrDefault(),
                Description = c.Description,
                Flavors = c.Flavors,
                Id = c.Id,
                ImageUrl = c.Image,
                Ingredients = c.Ingredients,
                Name = c.Name,
                Price = c.Price,
                RestaurantId = c.RestaurantId
            };
        }

        internal static OrderedDishModel ToModel(OrderedDish o)
        {
            return new OrderedDishModel
            {
                CuisineId = o.CuisineId,
                DishId = o.DishId,
                Quantity = o.Quantity
            };
        }

        internal static RestaurantModel ToModel(Restaurant r)
        {
            return new RestaurantModel
            {
                Id = r.Id,
                CloverId = r.CloverId,
                ImageUrl = r.Image,
                Location = r.Location,
                Name = r.Name,
                Owner = r.Owner,
                Phone = r.Phone
            };
        }
    }
}
