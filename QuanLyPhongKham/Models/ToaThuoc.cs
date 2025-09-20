using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    public class ToaThuoc
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Mã toa")]
        public string MaToa { get; set; }

        [ForeignKey("HoSoKham")]
        [Display(Name = "Hồ sơ khám")]
        public int HoSoKhamId { get; set; }
        public virtual HoSoKham HoSoKham { get; set; }

        [Display(Name = "Ngày kê")]
        public DateTime NgayKe { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        [ForeignKey("DuocSi")]
        [Display(Name = "Dược sĩ")]
        public int DuocSiId { get; set; }
        public virtual DuocSi DuocSi { get; set; }

    }
}
