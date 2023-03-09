using DSCWebApp.DataAccess.Repository.IRepository;
using DSCWebApp.Models;

namespace DSCWebApp.DataAccess.Repository
{
    public class PostRepository : Repository<Post>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}