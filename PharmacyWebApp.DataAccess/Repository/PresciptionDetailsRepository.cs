using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class PresciptionDetailsRepository : Repository<PrescriptionDetails>, IPrescriptionDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public PresciptionDetailsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}