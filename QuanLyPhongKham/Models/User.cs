using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace QuanLyPhongKham.Models
{
    public class User
    {
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string HoTen { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
    }
}
