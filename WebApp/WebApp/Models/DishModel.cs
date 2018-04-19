namespace WebApp.Models
{
    public class DishModel
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int CuisineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Flavors { get; set; }
        public string Ingredients { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}