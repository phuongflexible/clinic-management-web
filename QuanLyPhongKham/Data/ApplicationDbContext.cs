using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BenhNhan> BenhNhan { get; set; }
        public DbSet<QuanLyPhongKham.Models.Bacsi> Bacsi { get; set; } = default!;
        public DbSet<QuanLyPhongKham.Models.DuocSi> DuocSi { get; set; } = default!;
        public DbSet<QuanLyPhongKham.Models.ThuNgan> ThuNgan { get; set; } = default!;
        public DbSet<QuanLyPhongKham.Models.LeTan> LeTan { get; set; } = default!;
        public DbSet<QuanLyPhongKham.Models.Thuoc> Thuoc { get; set; } = default!;
    }
}
