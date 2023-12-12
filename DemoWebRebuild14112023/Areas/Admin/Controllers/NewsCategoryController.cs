using DatabaseFirstDemo.Models;
using DatabaseFirstDemo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DemoWebRebuild14112023.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class NewsCategoryController : BaseController
    {
        INewsCategoryRepository newsCategoryRepository = null;
        public NewsCategoryController()
        {
            newsCategoryRepository = new NewsCategoryRepository();
        }
        public IActionResult Index()
        {
            var result = newsCategoryRepository.GetAll();
            result = result.OrderBy(c => c.CategoryName).ToList();
            return View(result);
        }

        // GET: Admin/Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(NewsCategory newCategory)
        {
            try
            {
                var isDuplicate = newsCategoryRepository.GetAll().Any(c => c.CategoryName == newCategory.CategoryName);

                if (isDuplicate)
                {
                    ModelState.AddModelError("CategoryName", "Tên tin tức đã tồn tại");
                    SetAlert("This news does exists","warning");
                    return Json(new { warning = true });

                }
                else
                {
                    newsCategoryRepository.Insert(newCategory);
                    SetAlert("Insert Data is success!", "success");
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = false });
        }

      /*  public IActionResult Edit(int id)
        {
            NewsCategory newCategory = newsCategoryRepository.GetById(id);
            return View(newCategory);
        }*/

        [HttpPost]
        public JsonResult Edit(NewsCategory newCategory)
        {
            try
            {
                
                    newsCategoryRepository.Update(newCategory);
                    SetAlert("Update Data is success!", "success");
                    return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public JsonResult Delete(NewsCategory newsCategory)
        {
            try
            {
                newsCategoryRepository.Delete(newsCategory);
                SetAlert("Delete Data is success", "success");
            }catch(Exception ex)
            {
                return Json(new { success = false,message = ex.Message});   
            }
            return Json(new {success = true});
        }


    }
}
