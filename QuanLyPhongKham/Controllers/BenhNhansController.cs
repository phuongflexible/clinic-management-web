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
    public class BenhNhansController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHost;

        public BenhNhansController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        
        // GET: BenhNhans
        public async Task<IActionResult> Index(int? pageNumber, string searchPhrase)
        {
            int pageSize = 5;
            int currentPage = pageNumber ?? 1;

            var query = _context.BenhNhan.AsQueryable();

            if (!string.IsNullOrEmpty(searchPhrase))
            {
                query = query.Where(b => b.HoTen.Contains(searchPhrase));
            }

            int totalCount = await query.CountAsync();
            var patients = await query.OrderBy(b => b.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var vm = new BenhNhanListViewModel
            {
                BenhNhans = patients,
                PageIndex = currentPage,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
            return View(vm);
        }

        // GET: BenhNhans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // GET: BenhNhans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BenhNhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgaySinh,GioiTinh,DiaChi,Avatar,HoTen,SDT,Email,Password")] BenhNhan benhNhan, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    //Lay duong dan thu muc wwwroot/uploads
                    string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    //Tao ten file
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string fileSavePath = Path.Combine(uploadsFolder, fileName);

                    //Luu file vao server
                    using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    benhNhan.Avatar = fileName; 
                } 
                
                _context.Add(benhNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(benhNhan);
        }

        // GET: BenhNhans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhan.FindAsync(id);
            if (benhNhan == null)
            {
                return NotFound();
            }
            return View(benhNhan);
        }

        // POST: BenhNhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NgaySinh,GioiTinh,DiaChi,Avatar,HoTen,SDT,Email,Password")] BenhNhan benhNhan, IFormFile file)
        {
            if (id != benhNhan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        //Lay duong dan thu muc wwwroot/uploads
                        string uploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        //Tao ten file
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string fileSavePath = Path.Combine(uploadsFolder, fileName);

                        //Luu file vao server
                        using (FileStream stream = new FileStream(fileSavePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        benhNhan.Avatar = fileName;
                    }
                    _context.Update(benhNhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenhNhanExists(benhNhan.Id))
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
            return View(benhNhan);
        }

        // GET: BenhNhans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // POST: BenhNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var benhNhan = await _context.BenhNhan.FindAsync(id);
            if (benhNhan != null)
            {
                _context.BenhNhan.Remove(benhNhan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenhNhanExists(int id)
        {
            return _context.BenhNhan.Any(e => e.Id == id);
        }

       
   
    }
}
