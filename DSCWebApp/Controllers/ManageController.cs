using Microsoft.AspNetCore.Mvc;

namespace DSCWebApp.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}