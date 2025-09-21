using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class DuocSi : User
    {
        [Required]   
        public int Id { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
