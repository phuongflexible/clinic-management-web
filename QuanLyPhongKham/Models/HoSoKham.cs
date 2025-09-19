using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    public class HoSoKham
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Mã hồ sơ khám")]
        public string MaHSK { get; set; }

        [ForeignKey("BenhNhan")]
        [Display(Name = "Bệnh nhân")]
        public int BenhNhanId { get; set; }
        public virtual BenhNhan BenhNhan { get; set; }

        [Display(Name = "Triệu chứng")]
        public string TrieuChung {  get; set; }

        [Display(Name = "Chẩn đoán")]
        public string ChanDoan { get; set; }

        [Display(Name = "Kết luận")]
        public string KetLuan {  get; set; }

        [Display(Name = "Ngày khám")]
        public DateTime NgayKham { get; set; }

        [ForeignKey("BacSi")]
        [Display(Name = "Bác sĩ")]
        public int BacSiId { get; set; }
        public virtual Bacsi Bacsi { get; set; }


    }
}
