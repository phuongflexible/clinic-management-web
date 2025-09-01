namespace QuanLyPhongKham.Models
{
    public class BacSiListViewModel
    {
        public IEnumerable<Bacsi> Doctors { get; set; } = new List<Bacsi>();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
