using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;

namespace QuanLyPhongKham.Models
{
    public class LichHenListViewModel 
    {
        //Variables for pagination
        public IEnumerable<LichHen> Appointments { get; set; } = new List<LichHen>();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

      
    }
}
