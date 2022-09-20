using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using System.Diagnostics;

namespace PharmacyWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Order> obj = _unitOfWork.Order.GetAll();
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
        public IActionResult Create(Order obj )
        {
            if (obj == null)
                obj = new Order();
            _unitOfWork.Order.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Order created successfully";
            return View(obj);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Order.GetFirstOrDefault(u => u.Id == id);

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
            var obj = _unitOfWork.Order.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Order.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Order deleted successfully";
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            var obj = _unitOfWork.Order.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Order obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Order.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Order updated successfully";
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
