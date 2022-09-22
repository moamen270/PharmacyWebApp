using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository;
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

        public IActionResult Index()
        {
            IEnumerable<Brand> obj = _unitOfWork.Brand.GetAll();
            return View(obj);
        }
       
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            
            
            _unitOfWork.Brand.Add(new Brand());
            _unitOfWork.Save();
            TempData["success"] = "Brand created successfully";
            return RedirectToAction("Index");
        }



        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var brand = _unitOfWork.Brand.GetFirstOrDefault(item => item.Id == id);
            if (brand == null)
                return Json(new { success = false, message = "Error While Deleting" });

            _unitOfWork.Brand.Delete(brand);
            _unitOfWork.Save();

            return Json(new { success = true, message = "brand Deleted Successfully" });

        }



        public IActionResult Edit(int? id)
        {           
            var obj = _unitOfWork.Brand.GetFirstOrDefault(u => u.Id == id);

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
