using Microsoft.AspNetCore.Mvc;
using DSCWebApp.DataAccess;
using DSCWebApp.DataAccess.Repository.IRepository;
using DSCWebApp.Models;
using System.Diagnostics;

namespace DSCWebApp.Controllers
{
    public class ViewController : Controller
    {
        private readonly ILogger<ViewController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ViewController(ILogger<ViewController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int? pageNumber = 1)
        {
            var products = await _unitOfWork.Post.GetAllAsync(new string[] { "Category" });
            int pageSize = 9;
            return View(await PaginatedList<Post>.CreateAsync(products, pageNumber ?? 1, pageSize));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}