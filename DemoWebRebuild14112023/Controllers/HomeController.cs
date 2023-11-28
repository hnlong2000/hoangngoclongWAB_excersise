using DemoWebRebuild14112023.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoWebRebuild14112023.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (model.UserName.Contains("admin") && model.Password.Contains("admin"))
            {
                TempData["Info"] = "Admin";
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            var list = ProductDAO.Instance.GetAllProducts().OrderByDescending(p=>p.Name);

            return View(list);
        }

        public IActionResult Privacy()
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