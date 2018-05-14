using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Controllers;

namespace WebApp.Models
{
    public class OrderHistoryModel
    {
        public DateTime DateTime { get; set; }
        public string OrderId { get; set; }
        public string Table { get; set; }
        public List<OrderedDishModel> Details { get; set; }
        public int TipInPennies { get; set; }
        public int TotalInPennies { get; set; }
    }
}