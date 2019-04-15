using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiCors.Models;

namespace WebApiCors.Controllers
{
    public class ProductsController : ApiController
    {
        private List<Product> products = new List<Product>();

        public ProductsController()
        {
            products.Add(new Product { Id = 1, Name = "Produto 1" });
            products.Add(new Product { Id = 2, Name = "Produto 2" });
            products.Add(new Product { Id = 3, Name = "Produto 3" });
            products.Add(new Product { Id = 4, Name = "Produto 4" });
            products.Add(new Product { Id = 5, Name = "Produto 5" });
            products.Add(new Product { Id = 6, Name = "Produto 6" });
            products.Add(new Product { Id = 7, Name = "Produto 7" });
            products.Add(new Product { Id = 8, Name = "Produto 8" });
        }

        [EnableCors(origins: "http://localhost:55078", headers: "*", methods: "*")]
        public IList<Product> Get()
        {
            return products;
        }
    }
}
