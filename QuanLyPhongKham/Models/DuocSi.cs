using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class DuocSi : User
    {
        [Required]   
        public int Id { get; set; }
    }
}
