using DSCWebApp.DataAccess.Repository.IRepository;

namespace DSCWebApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Post { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Post = new PostRepository(_context);
            Category = new CategoryRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}