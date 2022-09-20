using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<ShopCart> obj = _unitOfWork.ShopCart.GetAll();
            return View(obj);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShopCart obj = null)
        {
            if (obj == null)
                obj = new ShopCart();
            _unitOfWork.ShopCart.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "ShopCart created successfully";
            return View(obj);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _unitOfWork.ShopCart.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.ShopCart.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.ShopCart.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "ShopCart deleted successfully";
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            var obj = _unitOfWork.ShopCart.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShopCart obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ShopCart.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "ShopCart updated successfully";
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
