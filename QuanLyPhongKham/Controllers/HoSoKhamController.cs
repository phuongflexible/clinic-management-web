using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    public class HoSoKhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoSoKhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HoSoKham
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HoSoKham.Include(h => h.Bacsi).Include(h => h.BenhNhan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HoSoKham/Details/5
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoSoKham = await _context.HoSoKham
                .Include(h => h.Bacsi)
                .Include(h => h.BenhNhan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoSoKham == null)
            {
                return NotFound();
            }

            return View(hoSoKham);
        }

        // GET: HoSoKham/Create
        [Authorize(Roles = "Doctor")]
        public IActionResult Create()
        {
            ViewData["BacSiId"] = new SelectList(_context.Bacsi, "Id", "HoTen");
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhan, "Id", "HoTen");
            return View();
        }

        //Check if medical record code existed
        public async Task<bool> CheckIfMedicalRecordCodeExisted(string MaHSK)
        {
            return await _context.HoSoKham.AnyAsync(h => h.MaHSK == MaHSK);
        }

        // POST: HoSoKham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Create([Bind("Id,MaHSK,BenhNhanId,TrieuChung,ChanDoan,KetLuan,NgayKham,BacSiId")] HoSoKham hoSoKham)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                ViewData["BacSiId"] = new SelectList(_context.Bacsi, "Id", "HoTen", hoSoKham.BacSiId);
                ViewData["BenhNhanId"] = new SelectList(_context.BenhNhan, "Id", "HoTen", hoSoKham.BenhNhanId);

                if (await CheckIfMedicalRecordCodeExisted(hoSoKham.MaHSK))
                {
                    ModelState.AddModelError("MaHSK", "Mã hồ sơ khám bệnh đã tồn tại");
                    return View(hoSoKham);
                }
                _context.Add(hoSoKham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(hoSoKham);
        }

        // GET: HoSoKham/Edit/5
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoSoKham = await _context.HoSoKham.FindAsync(id);
            if (hoSoKham == null)
            {
                return NotFound();
            }
            ViewData["BacSiId"] = new SelectList(_context.Bacsi, "Id", "HoTen", hoSoKham.BacSiId);
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhan, "Id", "HoTen", hoSoKham.BenhNhanId);
            return View(hoSoKham);
        }

        // POST: HoSoKham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaHSK,BenhNhanId,TrieuChung,ChanDoan,KetLuan,NgayKham,BacSiId")] HoSoKham hoSoKham)
        {
            if (id != hoSoKham.Id)
            {
                return NotFound();
            }
            ModelState.Clear();
            ViewData["BacSiId"] = new SelectList(_context.Bacsi, "Id", "HoTen", hoSoKham.BacSiId);
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhan, "Id", "HoTen", hoSoKham.BenhNhanId);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoKham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoKhamExists(hoSoKham.Id))
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
            return View(hoSoKham);
        }

        // GET: HoSoKham/Delete/5
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoSoKham = await _context.HoSoKham
                .Include(h => h.Bacsi)
                .Include(h => h.BenhNhan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoSoKham == null)
            {
                return NotFound();
            }

            return View(hoSoKham);
        }

        // POST: HoSoKham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoSoKham = await _context.HoSoKham.FindAsync(id);
            if (hoSoKham != null)
            {
                _context.HoSoKham.Remove(hoSoKham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoKhamExists(int id)
        {
            return _context.HoSoKham.Any(e => e.Id == id);
        }
    }
}
