using System.ComponentModel.DataAnnotations;

namespace QuanLyPhongKham.Models
{
    public enum TrangThaiLichHen
    {
        [Display(Name = "Chờ xác nhận")]
        ChoXacNhan = 0,

        [Display(Name = "Đã xác nhận")]
        DaXacNhan = 1,

        [Display(Name = "Đã hủy")]
        DaHuy = 2
    }
}
