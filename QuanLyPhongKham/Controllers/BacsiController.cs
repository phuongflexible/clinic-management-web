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
    public class BacsiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BacsiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bacsi
        public async Task<IActionResult> Index(int? pageNumber, String searchPhrase)
        {
            int pageSize = 5;
            int currentPage = pageNumber ?? 1;

            var query = _context.Bacsi.AsQueryable();

            if (!string.IsNullOrEmpty(searchPhrase))
            {
                query = query.Where(b => b.HoTen.Contains(searchPhrase));
            }

            int totalCount = await query.CountAsync();
            var doctors = await query.OrderBy(b => b.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var vm = new BacSiListViewModel
            {
                Doctors = doctors,
                PageIndex = currentPage,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            return View(vm);
        }

        // GET: Bacsi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacsi = await _context.Bacsi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bacsi == null)
            {
                return NotFound();
            }

            return View(bacsi);
        }

        // GET: Bacsi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bacsi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChuyenKhoa,HoTen,SDT,Email,Password")] Bacsi bacsi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bacsi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bacsi);
        }

        // GET: Bacsi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacsi = await _context.Bacsi.FindAsync(id);
            if (bacsi == null)
            {
                return NotFound();
            }
            return View(bacsi);
        }

        // POST: Bacsi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChuyenKhoa,HoTen,SDT,Email,Password")] Bacsi bacsi)
        {
            if (id != bacsi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bacsi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BacsiExists(bacsi.Id))
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
            return View(bacsi);
        }

        // GET: Bacsi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bacsi = await _context.Bacsi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bacsi == null)
            {
                return NotFound();
            }

            return View(bacsi);
        }

        // POST: Bacsi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bacsi = await _context.Bacsi.FindAsync(id);
            if (bacsi != null)
            {
                _context.Bacsi.Remove(bacsi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BacsiExists(int id)
        {
            return _context.Bacsi.Any(e => e.Id == id);
        }
    }
}
