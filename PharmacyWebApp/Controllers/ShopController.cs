using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System.Diagnostics;
using PharmacyWebApp.DataAccess;

namespace PharmacyWebApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public ShopController(ILogger<ShopController> logger, IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber = 1)
        {
            var products = await _unitOfWork.Product.GetAllAsync(new string[] {"Category"});                 
            int pageSize = 9;
            return View(await PaginatedList<Product>.CreateAsync(products, pageNumber ?? 1, pageSize));            
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}