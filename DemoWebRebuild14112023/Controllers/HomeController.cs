using AutoMapper;
using DatabaseFirstDemo.Models;
using DemoWebRebuild14112023.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DemoWebRebuild14112023.Models;

namespace DemoWebRebuild14112023.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //private readonly IMapper mapper;
        //private static List<User> listUser = new List<User>();
        //public HomeController(IMapper mapper)
        //{
        //    this.mapper = mapper;
        //}
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Login(LoginModel model)
        //{
        //    if (model.UserName.Contains("admin") && model.Password.Contains("admin"))
        //    {
        //        TempData["Info"] = "Admin";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(model);
        //}

        //public IActionResult List()
        //{
        //    var list = ProductDao.Instance.GetAllProducts().OrderByDescending(p => p.Price);
        //    return View(list);
        //}

        public IActionResult Index()
        {
            return View();
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


        //public IActionResult GetAllUser()
        //{
        //    var listproductbrief = from p in listUser
        //                           select mapper.Map<UserViewModel>(p);
        //    return View(listproductbrief);
        //}

        //public IActionResult ListAll()
        //{
        //    return View(listUser);
        //}


        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Register(UserViewModel model)
        //{
        //    User user = mapper.Map<User>(model);
        //    user.RoleId = 3;
        //    listUser.Add(user);
        //    return RedirectToAction("ListAll");
        //}

    }
}