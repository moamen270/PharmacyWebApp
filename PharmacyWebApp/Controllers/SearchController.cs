using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class SearchController : Controller
    {
       

        private readonly ILogger<ShopController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public SearchController(ILogger<ShopController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }


        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Result(string key)
        {
            if (key == null)
            {
                var products = await _unitOfWork.Product.GetAllAsync(new string[] { "Category", "Brand" });
                return View(await PaginatedList<Product>.CreateAsync(products, 1, 9));
            }

            var products2 = await _unitOfWork.Product.GetAllByFilterAsync(e=> e.Name.Contains(key),new string[] { "Category", "Brand" });
            return View(await PaginatedList<Product>.CreateAsync(products2, 1, 9));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}