namespace QuanLyPhongKham.Models
{
    public class ThuocListViewModel
    {
        public IEnumerable<Thuoc> Drugs { get; set; } = new List<Thuoc>();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
