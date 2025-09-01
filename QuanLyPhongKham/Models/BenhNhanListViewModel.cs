namespace QuanLyPhongKham.Models
{
    public class BenhNhanListViewModel
    {
        public IEnumerable<BenhNhan> BenhNhans { get; set; } = new List<BenhNhan>(); 
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
