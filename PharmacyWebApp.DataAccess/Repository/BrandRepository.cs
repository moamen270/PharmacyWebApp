using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}