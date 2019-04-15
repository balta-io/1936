using System.Collections.Generic;

namespace WebApplication3.Models
{
    public class DashboardResultViewModel
    {
        public string User { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> ProductsUserLiked { get; set; }
    }
}