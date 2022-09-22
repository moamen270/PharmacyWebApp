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

        public IActionResult Index()
        {
            IEnumerable<Category> obj = _unitOfWork.Category.GetAll();
            return View(obj);
        }
        //POST
        [HttpPost]
        public IActionResult Create()
        {
            _unitOfWork.Category.Add(new Category());
            _unitOfWork.Save();

            return Json(new { success = true, message = "Create Created Successfully" });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var Category = _unitOfWork.Category.GetFirstOrDefault(item => item.Id == id);
            if (Category == null)
                return Json(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Category.Delete(Category);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Brand Deleted Successfully" });

        }


        public IActionResult Edit(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

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
