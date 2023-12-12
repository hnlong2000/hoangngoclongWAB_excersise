using DatabaseFirstDemo.Models;
using DatabaseFirstDemo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using X.PagedList;


namespace DemoWebRebuild14112023.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class NewsController : BaseController
    {
        INewsRepository newsRepository = null;
        IUsersRepository userRepository = null;
        private readonly IWebHostEnvironment webHostEnvironment;
        public NewsController(IWebHostEnvironment webHostEnvironment)
        {
            newsRepository = new NewsRepository();
            userRepository = new UsersRepository();
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string? searchString, int? page, string sortBy, int? categoryId)
        {

            IEnumerable<NewsCategory> newsCategory = newsRepository.GetAllNewsCategory();
            // Tạo SelectList từ danh sách quyền truy cập
            SelectList selectList = new SelectList(newsCategory, "Id", "CategoryName");

            // Lưu SelectList vào ViewBag để sử dụng trong View
            ViewBag.NewsCategory = selectList;
            TempData["searchString"] = searchString != null ? searchString.ToLower() : "";
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            IPagedList<News> newsList = new PagedList<News>(newsRepository.GetNewsByKeyword(searchString, sortBy, categoryId), pageNumber, pageSize);
            return View(newsList);
        }


        // GET: Admin/Roles/Create
        public IActionResult Create()
        {
            ViewBag.NewsCategory = new SelectList(newsRepository.GetAllNewsCategory(), "Id", "CategoryName");
            return View();
        }

        [HttpPost]
        public JsonResult Create(News news)
        {
            try
            {
                // Lấy giá trị từ các trường thuộc tính của news trong form
                var model = new News
                {
                    Title = news.Title,
                    Descripition = news.Descripition,
                    SubjectContent = news.SubjectContent,
                    CategoryId = news.CategoryId,
                    Avatar = news.Avatar,
                    Status = news.Status
                };
                model.DateUpdate = Common.Common.GetServerDateTime();
                var user = User as ClaimsPrincipal;
                var userName = user?.FindFirstValue(ClaimTypes.Name);
                model.UserId = userRepository.GetByUserName(userName).Id;
                //string uniqueFileName = UploadedFile(model);
                //model.Avatar = uniqueFileName;
                newsRepository.Insert(model);

                SetAlert("Insert Data is success!", "success");

                /* }*/
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            News news = newsRepository.GetById(id);
            ViewBag.NewsCategory = new SelectList(newsRepository.GetAllNewsCategory(), "Id", "CategoryName");
            var data = new
            {
                Id = id,
                Title = news.Title,
                Descripition = news.Descripition,
                SubjectContent = news.SubjectContent,
                CategoryId = news.CategoryId,
                DateUpdate = news.DateUpdate.ToString("dd/MM/yyyy"),
                Avatar = news.Avatar,
                Status = news.Status,
                UserId = news.UserId,
            };
            ViewBag.Anh = news.Avatar;
            return new JsonResult(new { success = true, data = data });
        }

        [HttpPost]
        public JsonResult Edit(News news)
        {
            try
            {
                // Lấy giá trị từ các trường thuộc tính của user trong form
                var newp = new News
                {
                    Id = news.Id,
                    Title = news.Title,
                    Descripition = news.Descripition,
                    SubjectContent = news.SubjectContent,
                    CategoryId = news.CategoryId,
                    Avatar = news.Avatar,
                    Status = news.Status,
                    UserId = news.UserId,
                };
                if (news.DateUpdate != null)
                {
                    newp.DateUpdate = DateTime.ParseExact(news.DateUpdate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null);
                }
                //string uniqueFileName = UploadedFile(news);
                //news.Avatar = ViewBag.Anh;
                /*var user = User as ClaimsPrincipal;
                var userName = user?.FindFirstValue(ClaimTypes.Name);
                newp.UserId = userRepository.GetByUserName(userName).UserId;*/
                newsRepository.Update(newp);
                SetAlert("Update Data is success!", "success");

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        private string UploadedFile(News news)
        {
            //string uniqueFileName = UploadedFile(hh);
            //Save image to wwwroot/image
            string wwwRootPath = webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(news.ImageFile.FileName);
            string extension = Path.GetExtension(news.ImageFile.FileName);
            news.Avatar = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Upload/Images/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                news.ImageFile.CopyTo(fileStream);
            }
            ViewBag.Anh = news.Avatar;
            return fileName;
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {

                string wwwRootPath = webHostEnvironment.WebRootPath;
                // Tạo thư mục nếu nó không tồn tại
                /* if (!Directory.Exists(uploadsFolder))
                 {
                     Directory.CreateDirectory(uploadsFolder);
                 }*/
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                //var filePath = Path.Combine(uploadsFolder, file.FileName);
                string extension = Path.GetExtension(file.FileName);
                ViewBag.Anh = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Upload/Images/", fileName);
                // Lưu tệp tin vào thư mục uploads
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var data = new
                {
                    Avatar = fileName,
                };
                return new JsonResult(new { success = true, data = data });
                // Trả về phản hồi thành công (nếu cần)
                //return Ok();
            }

            // Trả về phản hồi lỗi (nếu có)
            return BadRequest();
        }

    }
}
