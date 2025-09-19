using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    public class ChiTietToaThuoc
    {
        [Required]
        public int Id { get; set; }

        [ForeignKey("ToaThuoc")]
        [Display(Name = "Toa thuốc")]
        public int ToaThuocId { get; set; }
        public virtual ToaThuoc ToaThuoc { get; set; }

        [ForeignKey("Thuoc")]
        [Display(Name = "Thuốc")]
        public int ThuocId { get; set; }
        public virtual Thuoc Thuoc { get; set; }

        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Cách dùng")]
        public string CachDung {  get; set; }
    }
}
