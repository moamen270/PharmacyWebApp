using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
