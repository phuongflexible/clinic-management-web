using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class LeTan : User
    {
        [Required]
        public int Id { get; set; }
    }
}
