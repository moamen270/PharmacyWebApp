using Microsoft.AspNetCore.Mvc;
using DSCWebApp.Models;
using System.Diagnostics;

namespace DSCWebApp.Controllers
{
    public class Donate : Controller
    {
        private readonly ILogger<Donate> _logger;

        public Donate(ILogger<Donate> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}