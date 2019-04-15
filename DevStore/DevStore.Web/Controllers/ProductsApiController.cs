using DevContas.Domain;
using DevContas.Domain.Repositories;
using DevStore.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevStore.Web.Controllers
{
    public class ProductsApiController : ApiController
    {
        private IProductRepository _repository;

        public ProductsApiController()
        {
            _repository = new ProductRepository();
        }

        public List<Product> Get()
        {
            return _repository.Get();
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
        }
    }
}
