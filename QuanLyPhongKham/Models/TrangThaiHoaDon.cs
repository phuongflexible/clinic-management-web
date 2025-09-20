using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public enum TrangThaiHoaDon
    {
        [Display(Name = "Chưa thanh toán")]
        ChuaThanhToan = 0,

        [Display(Name = "Đã thanh toán")]
        DaThanhToan = 1
    }
}
