using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepsitory
{
    public interface IProductRepository : IRepository<Product>
    {
        void Edit(Category obj);
     
    }
}
