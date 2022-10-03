using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class ShopCartRepository : Repository<ShopCart>, IShopCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShopCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}