using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;

namespace QuanLyPhongKham.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThongKeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongKeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> MonthlyStatistics(int year)
        {
            var doanhThu = await _context.HoaDon.Where(x => x.NgayLap.Year == year)
                .GroupBy(x => x.NgayLap.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    DoanhThu = g.Sum(x => x.TongTien)
                })
                .ToListAsync();

            var benhNhan = await _context.HoSoKham.Where(x => x.NgayKham.Year == year)
                .GroupBy(x => x.NgayKham.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    SoBenhNhan = g.Select(x => x.BenhNhanId).Distinct().Count()
                })
                .ToListAsync();
            return Json(new { DoanhThu = doanhThu, BenhNhan = benhNhan });
            //return View();
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
