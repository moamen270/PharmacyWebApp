using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
