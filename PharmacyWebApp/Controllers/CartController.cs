using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using PharmacyWebApp.Models;
using PharmacyWebApp.Models.ViewModels;
using PharmacyWebApp.Utility;
using System.Security.Claims;

namespace PharmacyWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public int OrderTotal { get; set; }

        public CartController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAllByDeafult(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"),
                OrderForHeader = new()  
            };
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price);
               ShoppingCartVM.OrderForHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }
        public IActionResult Buynow()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAllByDeafult(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"),
                OrderForHeader = new()
            };
            ShoppingCartVM.OrderForHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == claim.Value);

            ShoppingCartVM.OrderForHeader.Name = ShoppingCartVM.OrderForHeader.ApplicationUser.FirstName;

            ShoppingCartVM.OrderForHeader.PhoneNumber = ShoppingCartVM.OrderForHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderForHeader.StreetAddress = ShoppingCartVM.OrderForHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderForHeader.City = ShoppingCartVM.OrderForHeader.ApplicationUser.City;
            ShoppingCartVM.OrderForHeader.State = ShoppingCartVM.OrderForHeader.ApplicationUser.State;
            ShoppingCartVM.OrderForHeader.PostalCode = ShoppingCartVM.OrderForHeader.ApplicationUser.PostalCode;

            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price);
                ShoppingCartVM.OrderForHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }


        [HttpPost]
        [ActionName("Buynow")]
        [ValidateAntiForgeryToken]
        public IActionResult BuynowPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM.ListCart = _unitOfWork.ShoppingCart.GetAllByDeafult(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product");


            ShoppingCartVM.OrderForHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderForHeader.UserId = claim.Value;


            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price);
                ShoppingCartVM.OrderForHeader.OrderTotal += (cart.Price * cart.Count);
            }
            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);


                ShoppingCartVM.OrderForHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderForHeader.OrderStatus = SD.StatusApproved;

            _unitOfWork.OrderForHeader.Add(ShoppingCartVM.OrderForHeader);
            _unitOfWork.Save();
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                OrderForDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartVM.OrderForHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderForDetail.Add(orderDetail);
                _unitOfWork.Save();
            }
            _unitOfWork.ShoppingCart.DeleteRange(ShoppingCartVM.ListCart);
            _unitOfWork.Save();
            return RedirectToAction("Index","Home");
        }












        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Delete(cart);
                //var count = _unitOfWork.ShoppingCart.GetAllByDeafult(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;
                //HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
            else
            {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Delete(cart);
            _unitOfWork.Save();
            //var count = _unitOfWork.ShoppingCart.GetAllByDeafult(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            //HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToAction(nameof(Index));
        }





        private double GetPriceBasedOnQuantity(double quantity, double price)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else
            {

                return price;
            }
        }

    }
}
