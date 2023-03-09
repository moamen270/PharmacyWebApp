using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DSCWebApp.DataAccess.Repository.IRepository;
using DSCWebApp.Models;
using DSCWebApp.Models.ViewModels;
using System.Diagnostics;

namespace DSCWebApp.Controllers
{
	public class PostController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;

		public PostController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			IEnumerable<Post> obj = await _unitOfWork.Post.GetAllAsync(new string[] { "Category" });
			return View(obj);
		}

		//POST
		[HttpPost]
		public async Task<IActionResult> QuickCreate()
		{
			await _unitOfWork.Post.AddAsync(new Post
			{
				CategoryId = (await _unitOfWork.Category.GetFirstOrDefaultAsync()).Id,
			});
			_unitOfWork.Save();
			return Json(new { success = true, message = "Post Created Successfully" });
		}

		public async Task<IActionResult> Create()
		{
			PostVM viewModel = new PostVM
			{
				Categories = await _unitOfWork.Category.GetAllAsync(),
			};

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Create(PostVM viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Categories = await _unitOfWork.Category.GetAllAsync();
				return View(viewModel);
			}

			if (Request.Form.Files.Count > 0)
			{
				var file = Request.Form.Files.FirstOrDefault();

				//check file size and extension

				using var dataStream = new MemoryStream();
				await file.CopyToAsync(dataStream);
				viewModel.PostPicture = dataStream.ToArray();
			}

			Post product = new()
			{
				Title = viewModel.Title,
				Description = viewModel.Description,
				CategoryId = viewModel.CategoryId,
				PostPicture = viewModel.PostPicture
			};

			await _unitOfWork.Post.AddAsync(product);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}

		//POST
		[HttpDelete]
		public async Task<IActionResult> Delete(int? id)
		{
			var obj = await _unitOfWork.Post.GetFirstOrDefaultAsync(u => u.Id == id);
			if (obj == null)
			{
				return Json(new { success = true, message = "Faild" });
			}
			_unitOfWork.Post.Delete(obj);
			_unitOfWork.Save();
			TempData["success"] = "Post deleted successfully";
			return Json(new { success = true, message = "Post Deleted Successfully" });
		}

		public async Task<IActionResult> Edit(int id)
		{
			var obj = await _unitOfWork.Post.GetFirstOrDefaultAsync(u => u.Id == id);
			if (obj == null)
				return NotFound();

			PostVM viewModel = new PostVM
			{
				Id = obj.Id,
				Title = obj.Title,
				Description = obj.Description,
				CategoryId = obj.CategoryId,
				PostPicture = obj.PostPicture,
				Categories = await _unitOfWork.Category.GetAllAsync(),
			};

			return View(viewModel);
		}

		//POST
		[HttpPost]
		public async Task<IActionResult> Edit(PostVM viewModel)
		{
			if (!ModelState.IsValid)
			{
				viewModel.Categories = await _unitOfWork.Category.GetAllAsync();
				return View(viewModel); ;
			}

			Post product = await _unitOfWork.Post.GetFirstOrDefaultAsync(p => p.Id == viewModel.Id);
			if (Request.Form.Files.Count > 0)
			{
				var file = Request.Form.Files.FirstOrDefault();

				//check file size and extension

				using var dataStream = new MemoryStream();
				await file.CopyToAsync(dataStream);
				product.PostPicture = dataStream.ToArray();
			}
			product.Title = viewModel.Title;
			product.Description = viewModel.Description;
			product.CategoryId = viewModel.CategoryId;
			_unitOfWork.Post.Update(product);
			_unitOfWork.Save();
			TempData["success"] = "Post updated successfully";
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(int id)
		{
			Post obj = await _unitOfWork.Post.GetFirstOrDefaultAsync(p => p.Id == id, new string[] { "Category" });
			PostVM viewModel = new()
			{
				Title = obj.Title,
				CategoryId = obj.CategoryId,
				Description = obj.Description,
				PostPicture = obj.PostPicture,
				Category = obj.Category,
			};
			return View(viewModel);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}