using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class OrderRepository : Repository<Order> ,IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
