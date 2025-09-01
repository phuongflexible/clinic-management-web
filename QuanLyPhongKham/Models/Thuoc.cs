using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public class Thuoc
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Tên thuốc")]
        public string TenThuoc { get; set; }

        [Display(Name = "Đơn vị tính")]
        public string DonViTinh { get; set; }

        [Display(Name = "Giá tiền")]
        public double Gia { get; set; }

        [Display(Name = "Số lượng tồn kho")]
        public double SoLuongTon { get; set; }
    }
}
