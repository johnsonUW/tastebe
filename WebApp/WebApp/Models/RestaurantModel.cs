namespace WebApp.Models
{
    public class RestaurantModel
    {
        public int Id { get; set; }
        public string CloverId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Owner { get; set; }
        public string ImageUrl { get; set; }
        public string AccessToken { get; set; }
        public bool IsSandbox { get; set; }
        public double ExchangeRate { get; set; }
        public int OutstandingBalance { get; set; }
    }
}