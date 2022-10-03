using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class OrderForDetailRepository : Repository<OrderForDetail>, IOrderForDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderForDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}