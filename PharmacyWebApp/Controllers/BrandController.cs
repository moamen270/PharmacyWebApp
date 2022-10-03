using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Brand> obj = await _unitOfWork.Brand.GetAllAsync();
            return View(obj);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await _unitOfWork.Brand.AddAsync(new Brand());
            _unitOfWork.Save();

            return Json(new { success = true, message = "Brand Created Successfully" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var brand = await _unitOfWork.Brand.GetFirstOrDefaultAsync(item => item.Id == id);
            if (brand == null)
                return Json(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Brand.Delete(brand);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Brand Deleted Successfully" });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var obj = await _unitOfWork.Brand.GetFirstOrDefaultAsync(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Brand.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Brand updated successfully";
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