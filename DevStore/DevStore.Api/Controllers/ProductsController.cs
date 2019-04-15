using DevContas.Domain;
using DevContas.Domain.Repositories;
using DevContas.Domain.Services;
using DevStore.Api.Models;
using DevStore.Data.Repositories;
using DevStore.Service;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace DevStore.Api.Controllers
{
    [RoutePrefix("api")]
    public class ProductsController : ApiController
    {
        private IProductRepository _repository = new ProductRepository();

        // GET: api/Products
        [Route("products/{skip}/{take}")]
        public IList<Product> GetProducts(int skip = 0, int take = 25)
        {
            return _repository.Get(skip, take);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = _repository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return _repository.Get().Count(e => e.Id == id) > 0;
        }
    }
}