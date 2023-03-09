using Microsoft.AspNetCore.Mvc;
using DSCWebApp.DataAccess;
using DSCWebApp.DataAccess.Repository.IRepository;
using DSCWebApp.Models;
using System.Diagnostics;

namespace DSCWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<ViewController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public SearchController(ILogger<ViewController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string searchString)
        {
            ViewData["SearchString"] = searchString;
            return View();
        }

        public async Task<IActionResult> Result(string searchString)
        {
            ViewData["SearchString"] = searchString;
            if (searchString == null)
            {
                ViewData["ResultCount"] = "";
                var products = await _unitOfWork.Post.GetAllAsync(new string[] { "Category" });
                return View(await PaginatedList<Post>.CreateAsync(products, 1, 9));
            }

            var productsResult = await _unitOfWork.Post.GetAllByFilterAsync(e => e.Title.Contains(searchString), new string[] { "Category" });
            if (productsResult.FirstOrDefault() != null)
            {
                ViewData["ResultCount"] = productsResult.Count();
                return View(await PaginatedList<Post>.CreateAsync(productsResult, 1, 9));
            }
            else
                return RedirectToAction(nameof(Index), new { searchString });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}