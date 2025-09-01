using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class BenhNhan : User
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Ngày sinh")]
        public string NgaySinh { get; set; }

        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [Display(Name = "Địa chỉ")]
        public string DiaChi{ get; set; }

        [Display(Name = "Hình đại diện")]
        public string? Avatar { get; set; }

        public BenhNhan() { }
    }
}
