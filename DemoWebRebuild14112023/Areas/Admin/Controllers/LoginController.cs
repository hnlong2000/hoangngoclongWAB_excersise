using Microsoft.AspNetCore.Mvc;
using DemoWebRebuild14112023.Models;
using DatabaseFirstDemo.Repository;
using DatabaseFirstDemo.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace DemoWebRebuild14112023.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : BaseController
    {
        IUsersRepository usersRepository = null;
        public LoginController()
        {
            usersRepository = new UsersRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            var username = model.UserName;
            var password = model.Password;
            User user = usersRepository.CheckLogin(username, Common.Common.EncryptMD5(password));

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                {
                    return RedirectToAction("Index");
                }

                if (user != null)
                {
                    var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                    var indentity = new ClaimsIdentity(claims, "Admin");

                    var principal = new ClaimsPrincipal(indentity);

                    HttpContext.SignInAsync("Admin", principal, new AuthenticationProperties()
                    {
                        IsPersistent = true,
                    });

                    SetAlert("Đăng nhập thành công!", "success");
                    var routeValue = new RouteValueDictionary()
                {
                    {"area", "Admin" },
                    {"returnURL", Request.Query["ReturnURL"] },
                    {"claimvalue","true" },
                };

                    TempData["Info"] = "Admin";
                    return RedirectToAction("Index", "Home", routeValue);

                }
                else
                {
                    SetAlert("Tên đăng nhập và mật khẩu không đúng!", "error");
                }

             }
                else
                {
                    ModelState.AddModelError("Error",
                                     "Please input field full!");
                }
                return View(model);
            }
        }  
}
