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
    public class DuocSiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DuocSiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DuocSi
        public async Task<IActionResult> Index()
        {
            return View(await _context.DuocSi.ToListAsync());
        }

        // GET: DuocSi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duocSi = await _context.DuocSi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duocSi == null)
            {
                return NotFound();
            }

            return View(duocSi);
        }

        // GET: DuocSi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DuocSi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoTen,SDT,Email,Password")] DuocSi duocSi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duocSi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duocSi);
        }

        // GET: DuocSi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duocSi = await _context.DuocSi.FindAsync(id);
            if (duocSi == null)
            {
                return NotFound();
            }
            return View(duocSi);
        }

        // POST: DuocSi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoTen,SDT,Email,Password")] DuocSi duocSi)
        {
            if (id != duocSi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duocSi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuocSiExists(duocSi.Id))
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
            return View(duocSi);
        }

        // GET: DuocSi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duocSi = await _context.DuocSi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duocSi == null)
            {
                return NotFound();
            }

            return View(duocSi);
        }

        // POST: DuocSi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duocSi = await _context.DuocSi.FindAsync(id);
            if (duocSi != null)
            {
                _context.DuocSi.Remove(duocSi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuocSiExists(int id)
        {
            return _context.DuocSi.Any(e => e.Id == id);
        }
    }
}
