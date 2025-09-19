using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhongKham.Models
{
    public class LichHen
    {
        public int Id { get; set; }

        [ForeignKey("BenhNhan")]
        [Display(Name = "Tên bệnh nhân")]
        public int BenhNhanId { get; set; }
        public virtual BenhNhan BenhNhan { get; set; }

        
        [ForeignKey("BacSi")]
        [Display(Name = "Tên bác sĩ")]
        public int BacSiId { get; set; }
        public virtual Bacsi BacSi { get; set; }

        [ForeignKey("LeTan")]
        [Display(Name = "Tên lễ tân")]
        public int LeTanId { get; set; }
        public virtual LeTan LeTan { get; set; }

        [Display(Name = "Ngày giờ")]
        [DataType(DataType.DateTime)]
        public DateTime NgayGio {  get; set; }

        [Display(Name = "Trạng thái")]
        public TrangThaiLichHen TrangThai { get; set; }

        public LichHen() { }

    }
}
