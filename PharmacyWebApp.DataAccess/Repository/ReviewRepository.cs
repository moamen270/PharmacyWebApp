using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class ReviewRepository : Repository<Review>,IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }
    }
}
