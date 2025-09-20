using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoaDonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HoaDon
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HoaDon.Include(h => h.HoSoKham).Include(h => h.ThuNgan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon
                .Include(h => h.HoSoKham)
                .Include(h => h.ThuNgan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDon/Create
        public IActionResult Create()
        {
            ViewData["HoSoKhamId"] = new SelectList(_context.HoSoKham, "Id", "MaHSK");
            ViewData["ThuNganId"] = new SelectList(_context.ThuNgan, "Id", "HoTen");
            return View();
        }

        //Check Bill Code if exist
        public async Task<bool> CheckIfBillCodeExisted(string MaHD)
        {
            return await _context.HoaDon.AnyAsync(h => h.MaHD == MaHD);
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaHD,ThuNganId,HoSoKhamId,NgayLap,TongTien,PaymentMethod,TrangThai")] HoaDon hoaDon)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                ViewData["HoSoKhamId"] = new SelectList(_context.HoSoKham, "Id", "MaHSK", hoaDon.HoSoKhamId);
                ViewData["ThuNganId"] = new SelectList(_context.ThuNgan, "Id", "HoTen", hoaDon.ThuNganId);
                if (await CheckIfBillCodeExisted(hoaDon.MaHD))
                {
                    ModelState.AddModelError("MaHD", "Mã hóa đơn đã tồn tại");
                    return View(hoaDon);
                }
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoaDon);
        }

        // GET: HoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["HoSoKhamId"] = new SelectList(_context.HoSoKham, "Id", "MaHSK", hoaDon.HoSoKhamId);
            ViewData["ThuNganId"] = new SelectList(_context.ThuNgan, "Id", "HoTen", hoaDon.ThuNganId);
            return View(hoaDon);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaHD,ThuNganId,HoSoKhamId,NgayLap,TongTien,PaymentMethod,TrangThai")] HoaDon hoaDon)
        {
            if (id != hoaDon.Id)
            {
                return NotFound();
            }
            ModelState.Clear();
            ViewData["HoSoKhamId"] = new SelectList(_context.HoSoKham, "Id", "MaHSK", hoaDon.HoSoKhamId);
            ViewData["ThuNganId"] = new SelectList(_context.ThuNgan, "Id", "HoTen", hoaDon.ThuNganId);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }    
            return View(hoaDon);
        }

        // GET: HoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon
                .Include(h => h.HoSoKham)
                .Include(h => h.ThuNgan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon != null)
            {
                _context.HoaDon.Remove(hoaDon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDon.Any(e => e.Id == id);
        }
    }
}
