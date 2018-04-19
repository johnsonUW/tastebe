using System.Collections.Generic;

namespace WebApp.Models
{
    public class OrderRequestModel
    {
        public int RestaurantId { get; set; }
        public string UserId { get; set; }
        public int Table { get; set; }
        public List<OrderedDishModel> Items { get; set; }
    }
}