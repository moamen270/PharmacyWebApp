using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DSCWebApp.DataAccess;
using DSCWebApp.DataAccess.Repository.IRepository;
using DSCWebApp.Models;
using DSCWebApp.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace DSCWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManger)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Index(int? pageNumber = 1)
		{
			var obj = await _unitOfWork.Post.GetAllAsync(new string[] { "Category" });
			int pageSize = 6;
			return View(await PaginatedList<Post>.CreateAsync(obj, pageNumber ?? 1, pageSize));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}