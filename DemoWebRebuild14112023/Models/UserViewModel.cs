using System.ComponentModel.DataAnnotations;

namespace DemoWebRebuild14112023.Models
{
    public class UserViewModel
    {
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }

    }
}