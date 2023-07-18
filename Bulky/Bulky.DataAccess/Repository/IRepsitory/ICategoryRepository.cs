using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepsitory
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Edit(Category obj);
    }
}
