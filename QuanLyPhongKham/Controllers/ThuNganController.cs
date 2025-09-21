using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ThuNganController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThuNganController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ThuNgan
        public async Task<IActionResult> Index()
        {
            return View(await _context.ThuNgan.ToListAsync());
        }

        // GET: ThuNgan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuNgan = await _context.ThuNgan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thuNgan == null)
            {
                return NotFound();
            }

            return View(thuNgan);
        }

        // GET: ThuNgan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ThuNgan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoTen,SDT,Email,Password")] ThuNgan thuNgan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thuNgan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thuNgan);
        }

        // GET: ThuNgan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuNgan = await _context.ThuNgan.FindAsync(id);
            if (thuNgan == null)
            {
                return NotFound();
            }
            return View(thuNgan);
        }

        // POST: ThuNgan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoTen,SDT,Email,Password")] ThuNgan thuNgan)
        {
            if (id != thuNgan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thuNgan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThuNganExists(thuNgan.Id))
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
            return View(thuNgan);
        }

        // GET: ThuNgan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thuNgan = await _context.ThuNgan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (thuNgan == null)
            {
                return NotFound();
            }

            return View(thuNgan);
        }

        // POST: ThuNgan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thuNgan = await _context.ThuNgan.FindAsync(id);
            if (thuNgan != null)
            {
                _context.ThuNgan.Remove(thuNgan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThuNganExists(int id)
        {
            return _context.ThuNgan.Any(e => e.Id == id);
        }
    }
}
