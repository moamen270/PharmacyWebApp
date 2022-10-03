using Microsoft.AspNetCore.Mvc;

namespace PharmacyWebApp.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}