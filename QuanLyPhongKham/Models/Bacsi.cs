using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace QuanLyPhongKham.Models
{
    public class Bacsi : User
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Chuyên khoa")]
        [Required(ErrorMessage = "")]
        public string ChuyenKhoa { get; set; }

    }
}
