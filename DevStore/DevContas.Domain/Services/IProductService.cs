using System;

namespace DevContas.Domain.Services
{
    public interface IProductService : IDisposable
    {
        void CreateNewProduct(string name);
    }
}
