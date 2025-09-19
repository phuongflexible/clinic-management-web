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
    public class ChiTietToaThuocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChiTietToaThuocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChiTietToaThuoc
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ChiTietToaThuoc.Include(c => c.Thuoc).Include(c => c.ToaThuoc);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ChiTietToaThuoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietToaThuoc = await _context.ChiTietToaThuoc
                .Include(c => c.Thuoc)
                .Include(c => c.ToaThuoc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chiTietToaThuoc == null)
            {
                return NotFound();
            }

            return View(chiTietToaThuoc);
        }

        // GET: ChiTietToaThuoc/Create
        public IActionResult Create()
        {
            ViewData["ThuocId"] = new SelectList(_context.Thuoc, "Id", "TenThuoc");
            ViewData["ToaThuocId"] = new SelectList(_context.ToaThuoc, "Id", "Id");
            return View();
        }

        // POST: ChiTietToaThuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ToaThuocId,ThuocId,SoLuong,CachDung")] ChiTietToaThuoc chiTietToaThuoc)
        {
            if (ModelState.IsValid)
            {
                ViewData["ThuocId"] = new SelectList(_context.Thuoc, "Id", "TenThuoc", chiTietToaThuoc.ThuocId);
                ViewData["ToaThuocId"] = new SelectList(_context.ToaThuoc, "Id", "MaToa", chiTietToaThuoc.ToaThuocId);
                _context.Add(chiTietToaThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(chiTietToaThuoc);
        }
        // GET: ChiTietToaThuoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietToaThuoc = await _context.ChiTietToaThuoc.FindAsync(id);
            if (chiTietToaThuoc == null)
            {
                return NotFound();
            }
            ViewData["ThuocId"] = new SelectList(_context.Thuoc, "Id", "TenThuoc", chiTietToaThuoc.ThuocId);
            ViewData["ToaThuocId"] = new SelectList(_context.ToaThuoc, "Id", "MaToa", chiTietToaThuoc.ToaThuocId);
            return View(chiTietToaThuoc);
        }

        // POST: ChiTietToaThuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ToaThuocId,ThuocId,SoLuong,CachDung")] ChiTietToaThuoc chiTietToaThuoc)
        {
            if (id != chiTietToaThuoc.Id)
            {
                return NotFound();
            }
            ModelState.Clear();
            ViewData["ThuocId"] = new SelectList(_context.Thuoc, "Id", "TenThuoc", chiTietToaThuoc.ThuocId);
            ViewData["ToaThuocId"] = new SelectList(_context.ToaThuoc, "Id", "MaToa", chiTietToaThuoc.ToaThuocId);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietToaThuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietToaThuocExists(chiTietToaThuoc.Id))
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
           
            return View(chiTietToaThuoc);
        }

        // GET: ChiTietToaThuoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietToaThuoc = await _context.ChiTietToaThuoc
                .Include(c => c.Thuoc)
                .Include(c => c.ToaThuoc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chiTietToaThuoc == null)
            {
                return NotFound();
            }

            return View(chiTietToaThuoc);
        }

        // POST: ChiTietToaThuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietToaThuoc = await _context.ChiTietToaThuoc.FindAsync(id);
            if (chiTietToaThuoc != null)
            {
                _context.ChiTietToaThuoc.Remove(chiTietToaThuoc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietToaThuocExists(int id)
        {
            return _context.ChiTietToaThuoc.Any(e => e.Id == id);
        }
    }
}
