using Microsoft.AspNetCore.Identity;
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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> obj = await _unitOfWork.Product.GetAllAsync();
            return View(obj);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await _unitOfWork.Product.AddAsync(new Product
            {
                CategoryId = ( await _unitOfWork.Category.GetFirstOrDefaultAsync()).Id,
                BrandId = (await _unitOfWork.Brand.GetFirstOrDefaultAsync()).Id
            }) ; 
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product Created Successfully" });
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
            ProductVM productVM = new ProductVM
            {
                Product = new(),
                CategoryList = (await _unitOfWork.Category.GetAllAsync()).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                BrandList = (await _unitOfWork.Brand.GetAllAsync()).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            productVM.Product = await _unitOfWork.Product.GetFirstOrDefaultAsync(u => u.Id == id);
            return View(productVM);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files.FirstOrDefault();

                    //check file size and extension

                    using (var dataStream = new MemoryStream())
                    {
                         file.CopyToAsync(dataStream);
                        obj.Product.ProductPicture = dataStream.ToArray();
                    }

                    _unitOfWork.Product.Update(obj.Product);
                    _unitOfWork.Save();
                }

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
