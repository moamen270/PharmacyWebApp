using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int? pageNumber = 1)
        {
            var products = await _unitOfWork.Product.GetAllAsync(new string[] { "Category","Brand" });
            int pageSize = 6;
            return View(await PaginatedList<Product>.CreateAsync(products, pageNumber ?? 1, pageSize));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}