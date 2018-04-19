using System.Collections.Generic;

namespace WebApp.Models
{
    public class MenuModel
    {
        public CuisineModel Cuisine { get; set; }
        public List<DishModel> Items { get; set; }
    }
}