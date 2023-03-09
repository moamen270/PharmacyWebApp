using DSCWebApp.DataAccess.Repository.IRepository;
using DSCWebApp.Models;

namespace DSCWebApp.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}