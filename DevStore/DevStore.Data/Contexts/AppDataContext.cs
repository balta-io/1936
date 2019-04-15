using DevContas.Domain;
using System.Data.Entity;

namespace DevStore.Data.Contexts
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
            : base("AppCnnStr")
        { }

        public DbSet<Product> Products { get; set; }
    }
}
