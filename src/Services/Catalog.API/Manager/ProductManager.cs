using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using Catalog.API.Repository;
using MongoRepo.Manager;
using MongoRepo.Repository;

namespace Catalog.API.Manager
{
    public class ProductManager : CommonManager<Products>, IProductManager
    {
        public ProductManager() : base(new ProductRepository())
        {
        }

        internal void Add(object getPreconfiguredProducts)
        {
            throw new NotImplementedException();
        }
    }
}
