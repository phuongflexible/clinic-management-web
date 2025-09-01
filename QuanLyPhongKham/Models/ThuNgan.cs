using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class ThuNgan : User
    {
        [Required]
        public int Id { get; set; }
    }
}
