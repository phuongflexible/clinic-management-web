using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    public class ToaThuocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToaThuocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToaThuoc
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ToaThuoc.Include(t => t.DuocSi).Include(t => t.HoSoKham);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ToaThuoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toaThuoc = await _context.ToaThuoc
                .Include(t => t.DuocSi)
                .Include(t => t.HoSoKham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toaThuoc == null)
            {
                return NotFound();
            }

            return View(toaThuoc);
        }

        // GET: ToaThuoc/Create
        public IActionResult Create()
        {
            ViewData["DuocSiId"] = new SelectList(_context.DuocSi, "Id", "HoTen");
            ViewData["HoSoKhamId"] = new SelectList(_context.HoSoKham, "Id", "MaHSK");
            return View();
        }

        //Check prescription Code if exist
        public async Task<bool> CheckIfPrescriptionCodeExisted(string MaToa)
        {
            return await _context.ToaThuoc.AnyAsync(h => h.MaToa == MaToa);
        }

        // POST: ToaThuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaToa,HoSoKhamId,NgayKe,GhiChu,DuocSiId")] ToaThuoc toaThuoc)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                ViewData["DuocSiId"] = new SelectList(_context.DuocSi, "Id", "HoTen", toaThuoc.DuocSiId);
                ViewData["HoSoKhamId"] = new SelectList(_context.HoSoKham, "Id", "MaHSK", toaThuoc.HoSoKhamId);

                if (await CheckIfPrescriptionCodeExisted(toaThuoc.MaToa))
                {
                    ModelState.AddModelError("MaToa", "Mã toa thuốc đã tồn tại");
                    return View(toaThuoc);
                }
                _context.Add(toaThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toaThuoc);
        }

        // GET: ToaThuoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toaThuoc = await _context.ToaThuoc.FindAsync(id);
            if (toaThuoc == null)
            {
                return NotFound();
            }
            ViewData["DuocSiId"] = new SelectList(_context.DuocSi, "Id", "HoTen", toaThuoc.DuocSiId);
            ViewData["HoSoKhamId"] = new SelectList(_context.HoSoKham, "Id", "MaHSK", toaThuoc.HoSoKhamId);
            return View(toaThuoc);
        }

        // POST: ToaThuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaToa,HoSoKhamId,NgayKe,GhiChu,DuocSiId")] ToaThuoc toaThuoc)
        {
            if (id != toaThuoc.Id)
            {
                return NotFound();
            }
            ModelState.Clear();
            ViewData["DuocSiId"] = new SelectList(_context.DuocSi, "Id", "HoTen", toaThuoc.DuocSiId);
            ViewData["HoSoKhamId"] = new SelectList(_context.HoSoKham, "Id", "MaHSK", toaThuoc.HoSoKhamId);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toaThuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToaThuocExists(toaThuoc.Id))
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
            return View(toaThuoc);
        }

        // GET: ToaThuoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toaThuoc = await _context.ToaThuoc
                .Include(t => t.DuocSi)
                .Include(t => t.HoSoKham)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toaThuoc == null)
            {
                return NotFound();
            }

            return View(toaThuoc);
        }

        // POST: ToaThuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toaThuoc = await _context.ToaThuoc.FindAsync(id);
            if (toaThuoc != null)
            {
                _context.ToaThuoc.Remove(toaThuoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToaThuocExists(int id)
        {
            return _context.ToaThuoc.Any(e => e.Id == id);
        }
    }
}
