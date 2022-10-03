using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}