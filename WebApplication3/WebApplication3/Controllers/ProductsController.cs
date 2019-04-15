using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.OutputCache.V2;
using WebApplication3.Filters;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [RoutePrefix("api")]
    public class ProductsController : ApiController
    {
        private AppDataContext db = new AppDataContext();

        // GET: api/Products
        //[ApiAuthorizeFilter()]
        //[DeflateCompression]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)] //Install-Package Strathweb.CacheOutput.WebApi2
        [Route("v1/public/products")]
        public Task<HttpResponseMessage> GetProducts()
        {
            var response = new HttpResponseMessage();

            try
            {
                var products = db.Products.ToList();
                var productsUserLiked = db.Products.Where(x => x.Id > 30).ToList();
                var user = "andrebaltieri";

                var result = new DashboardResultViewModel
                {
                    User = user,
                    Products = products,
                    ProductsUserLiked = productsUserLiked
                };
                response =
                    Request
                    .CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                response =
                    Request
                    .CreateResponse(HttpStatusCode.BadRequest,
                    "Ops, não foi possível listar os produtos");
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Route("v2/public/products/user")]
        public HttpResponseMessage GetProductsByUser()
        {
            var response = new HttpResponseMessage();

            try
            {
                var result = db.Products.ToList();
                result.ForEach(x => x.Id = 0);
                response =
                    Request
                    .CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                response =
                    Request
                    .CreateResponse(HttpStatusCode.BadRequest,
                    "Ops, não foi possível listar os produtos");
            }


            return response;
        }

        [Route("v1/public/products/{name}")]
        public HttpResponseMessage GetProducts(string name)
        {
            var response = new HttpResponseMessage();

            try
            {
                var result =
                    db.Products
                    .Where(x => x.Name.StartsWith(name))
                    .ToList();
                response =
                    Request
                    .CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                response =
                    Request
                    .CreateResponse(HttpStatusCode.BadRequest,
                    "Ops, não foi possível listar os produtos");
            }


            return response;
        }

        [Route("v1/public/products/{skip}/{take}")]
        public HttpResponseMessage GetProducts(int skip = 0, int take = 25)
        {
            var response = new HttpResponseMessage();

            try
            {
                var result = db.Products.Skip(skip).Take(take).ToList();
                response =
                    Request
                    .CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                response =
                    Request
                    .CreateResponse(HttpStatusCode.BadRequest,
                    "Ops, não foi possível listar os produtos");
            }


            return response;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}