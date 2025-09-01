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
    public class LeTanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeTanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeTan
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeTan.ToListAsync());
        }

        // GET: LeTan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leTan = await _context.LeTan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leTan == null)
            {
                return NotFound();
            }

            return View(leTan);
        }

        // GET: LeTan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeTan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoTen,SDT,Email,Password")] LeTan leTan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leTan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leTan);
        }

        // GET: LeTan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leTan = await _context.LeTan.FindAsync(id);
            if (leTan == null)
            {
                return NotFound();
            }
            return View(leTan);
        }

        // POST: LeTan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoTen,SDT,Email,Password")] LeTan leTan)
        {
            if (id != leTan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leTan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeTanExists(leTan.Id))
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
            return View(leTan);
        }

        // GET: LeTan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leTan = await _context.LeTan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leTan == null)
            {
                return NotFound();
            }

            return View(leTan);
        }

        // POST: LeTan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leTan = await _context.LeTan.FindAsync(id);
            if (leTan != null)
            {
                _context.LeTan.Remove(leTan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeTanExists(int id)
        {
            return _context.LeTan.Any(e => e.Id == id);
        }
    }
}
