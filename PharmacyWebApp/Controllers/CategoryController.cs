using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> obj = await _unitOfWork.Category.GetAllAsync();
            return View(obj);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await _unitOfWork.Category.AddAsync(new Category());
            _unitOfWork.Save();

            return Json(new { success = true, message = "Create Created Successfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var Category = await _unitOfWork.Category.GetFirstOrDefaultAsync(item => item.Id == id);
            if (Category == null)
                return Json(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Category.Delete(Category);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Brand Deleted Successfully" });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var obj = await _unitOfWork.Category.GetFirstOrDefaultAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);

                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}