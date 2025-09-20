using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    public class HoaDon
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Mã hóa đơn")]
        public string MaHD { get; set; }

        [ForeignKey("ThuNgan")]
        [Display(Name = "Thu ngân")]
        public int ThuNganId { get; set; }
        public virtual ThuNgan ThuNgan { get; set; }

        [ForeignKey("HoSoKham")]
        [Display(Name = "Hồ sơ khám")]
        public int HoSoKhamId { get; set; }
        public virtual HoSoKham HoSoKham { get; set; }

        [Display(Name = "Ngày lập")]
        public DateTime NgayLap { get; set; }

        [Display(Name = "Tổng tiền")]
        public float TongTien { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod {  get; set; }

        [Display(Name = "Trạng thái")]
        public TrangThaiHoaDon TrangThai { get; set; }
    }
}
