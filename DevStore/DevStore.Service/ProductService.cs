using DevContas.Domain;
using DevContas.Domain.Repositories;
using DevContas.Domain.Services;
using DevStore.Data.Repositories;
namespace DevStore.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository _repo;
        public ProductService()
        {
            _repo = new ProductRepository();
        }

        public void CreateNewProduct(string name)
        {
            var product = new Product();
            product.Name = name;
            product.Id = 0;

            _repo.Create(product);
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}
