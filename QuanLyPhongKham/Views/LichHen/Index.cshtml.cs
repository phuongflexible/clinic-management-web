using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Models
{
    public class LichHenIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LichHenIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<LichHen> Appointments { get; set; } = new List<LichHen>();

        [BindProperty(SupportsGet = true)]
        public DateTime? FromDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? ToDate { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.LichHen
            .Include(l => l.BenhNhan)
            .Include(l => l.BacSi)
            .AsQueryable();

            if (FromDate.HasValue && ToDate.HasValue)
            {
                query = query.Where(l => l.NgayGio.Date >= FromDate.Value.Date
                                       && l.NgayGio.Date <= ToDate.Value.Date);
            }
            else if (FromDate.HasValue)
            {
                query = query.Where(l => l.NgayGio.Date >= FromDate.Value.Date);
            }
            else if (ToDate.HasValue)
            {
                query = query.Where(l => l.NgayGio.Date <= ToDate.Value.Date);
            }

            Appointments = await query.OrderBy(l => l.NgayGio).ToListAsync();

        }
    }
}
