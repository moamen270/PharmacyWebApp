using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using PharmacyWebApp.Models.ViewModels;
using System.Diagnostics;


namespace PharmacyWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> obj = _unitOfWork.Product.GetAll();
            return View(obj);
        }

        //POST
        [HttpPost]
        public IActionResult Create()
        {
            _unitOfWork.Product.Add(new Product
            {
                CategoryId = _unitOfWork.Category.GetFirstOrDefault().Id,
                BrandId = _unitOfWork.Brand.GetFirstOrDefault().Id
            }) ; 
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product Created Successfully" });
        }


        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = true, message = "Faild" });
            }
            _unitOfWork.Product.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return Json(new { success = true, message = "Product Deleted Successfully" });
        }


        public IActionResult Edit(int? id)
        {
            ProductVM productVM = new ProductVM
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                BrandList = _unitOfWork.Brand.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                
                if (obj.Product != null)
                {                
                   
                    _unitOfWork.Product.Update(obj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product updated successfully";
                    return RedirectToAction("Index");
                }
                
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
