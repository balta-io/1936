using DevContas.Domain;
using DevContas.Domain.Repositories;
using DevStore.Data.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace DevStore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private AppDataContext _db;

        public ProductRepository()
        {
            _db = new AppDataContext();
        }

        public List<Product> Get(int skip = 0, int take = 25)
        {
            return _db.Products
                .OrderBy(x=>x.Name)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Product Get(int id)
        {
            return _db.Products.Find(id);
        }

        public void Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            _db.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Products.Remove(Get(id));
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
