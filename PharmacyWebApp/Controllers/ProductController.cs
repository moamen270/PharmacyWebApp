using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using PharmacyWebApp.Models.ViewModels;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> obj = await _unitOfWork.Product.GetAllAsync(new string[] { "Brand", "Category" });
            return View(obj);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> QuickCreate()
        {
            await _unitOfWork.Product.AddAsync(new Product
            {
                CategoryId = (await _unitOfWork.Category.GetFirstOrDefaultAsync()).Id,
                BrandId = (await _unitOfWork.Brand.GetFirstOrDefaultAsync()).Id
            });
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product Created Successfully" });
        }

        public async Task<IActionResult> Create()
        {
            ProductVM viewModel = new ProductVM
            {
                Categories = await _unitOfWork.Category.GetAllAsync(),
                Brands = await _unitOfWork.Brand.GetAllAsync()
            };

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = await _unitOfWork.Category.GetAllAsync();
                viewModel.Brands = await _unitOfWork.Brand.GetAllAsync();
                return View(viewModel);
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                //check file size and extension

                using var dataStream = new MemoryStream();
                await file.CopyToAsync(dataStream);
                viewModel.ProductPicture = dataStream.ToArray();
            }

            Product product = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ListPrice = viewModel.ListPrice,
                BrandId = viewModel.BrandId,
                CategoryId = viewModel.CategoryId,
                ProductPicture = viewModel.ProductPicture
            };

            await _unitOfWork.Product.AddAsync(product);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        //POST
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var obj = await _unitOfWork.Product.GetFirstOrDefaultAsync(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = true, message = "Faild" });
            }
            _unitOfWork.Product.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return Json(new { success = true, message = "Product Deleted Successfully" });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(u => u.Id == id);
            if (product == null)
                return NotFound();

            ProductVM viewModel = new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ListPrice = product.ListPrice,
                Description = product.Description,
                ProductPicture = product.ProductPicture,
                Categories = await _unitOfWork.Category.GetAllAsync(),
                Brands = (await _unitOfWork.Brand.GetAllAsync())
            };

            return View("ProductForm", viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = await _unitOfWork.Category.GetAllAsync();
                viewModel.Brands = await _unitOfWork.Brand.GetAllAsync();
                return View("ProductForm", viewModel); ;
            }

            Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(p => p.Id == viewModel.Id);
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                //check file size and extension

                using var dataStream = new MemoryStream();
                await file.CopyToAsync(dataStream);
                product.ProductPicture = dataStream.ToArray();
            }
            product.Price = viewModel.Price;
            product.ListPrice = viewModel.ListPrice;
            product.Name = viewModel.Name;
            product.BrandId = viewModel.BrandId;
            product.Description = viewModel.Description;
            product.CategoryId = viewModel.CategoryId;
            product.Category = viewModel.Category;
            product.Brand = viewModel.Brand;
            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            Product product = await _unitOfWork.Product.GetFirstOrDefaultAsync(p => p.Id == id, new string[] { "Brand", "Category", "Reviews" });
            var RateList = await _unitOfWork.Review.GetAllByFilterAsync(a => a.ProductId == product.Id);
            ProductReviewVM viewModel = new()
            {
                Id = product.Id,
                Name = product.Name,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Price = product.Price,
                ListPrice = product.ListPrice,
                Description = product.Description,
                ProductPicture = product.ProductPicture,
                Category = product.Category,
                Brand = product.Brand,
                Reviews = (await _unitOfWork.Review.GetAllByFilterAsync(a => a.ProductId == product.Id && a.Comment != null, new string[] { "User" })).OrderByDescending(e => e.Rate),
            };
            double temp = 0;
            foreach (var item in RateList)
            {
                temp += item.Rate;
            }
            viewModel.AvgRate = temp / RateList.Count();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ProductReviewVM viewModel)
        {
            Review review = new Review()
            {
                ProductId = viewModel.Id,
                Rate = viewModel.Rate,
                Comment = viewModel.Comment,
                UserId = (await _userManager.GetUserAsync(User)).Id
            };
            await _unitOfWork.Review.AddAsync(review);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Details), new { id = viewModel.Id }); ;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}