using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace QuanLyPhongKham.Models
{
    public class Bacsi : User
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Chuyên khoa")]
        public string ChuyenKhoa { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
