using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepsitory;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _appDbContext;
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }
        public UnitOfWork(AppDbContext appDbContext) 
        {
            _appDbContext= appDbContext;
            Category = new CategoryRepository(_appDbContext);
            Product = new ProductRepository(_appDbContext);
        }
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
