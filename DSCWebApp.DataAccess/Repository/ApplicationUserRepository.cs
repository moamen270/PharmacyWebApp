using DSCWebApp.DataAccess.Repository.IRepository;
using DSCWebApp.Models;

namespace DSCWebApp.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}