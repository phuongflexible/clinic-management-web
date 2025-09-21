using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongKham.Data;
using QuanLyPhongKham.Models;

namespace QuanLyPhongKham.Controllers
{
    public class LichHenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LichHenController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin, Patient, Doctor, Receptionist")]
        // GET: LichHen
        public async Task<IActionResult> Index(int? pageNumber, String searchPhrase)
        {
            int pageSize = 5;
            int currentPage = pageNumber ?? 1;
            
            var query = _context.LichHen.AsQueryable();

            if (!string.IsNullOrEmpty(searchPhrase))
            {
                query = query.Where(l => l.BenhNhan.HoTen.Contains(searchPhrase));
            }
            int totalCount = await query.CountAsync();

            //Authorize
            if (User.IsInRole("Admin") || User.IsInRole("Receptionist"))
            {
                //Admin/Receptionist see all
                var allAppointments = await _context.LichHen
                    .Include(l => l.BenhNhan)
                    .OrderBy(l => l.NgayGio).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
                //admin, receptionist's own view
                var vm = new LichHenListViewModel
                {
                    Appointments = allAppointments,
                    PageIndex = currentPage,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };
                return View(vm);
            }
            else
            {
                //Patient only see theirs
                var userId = _userManager.GetUserId(User);
                var benhNhan = await _context.BenhNhan
                .FirstOrDefaultAsync(b => b.UserId == userId);
                if (benhNhan == null)
                {
                    return NotFound("Không tìm thấy bệnh nhân");
                }
                var myAppointments = await _context.LichHen
                    .Where(l => l.BenhNhanId == benhNhan.Id)
                    .OrderBy(l => l.NgayGio).Skip((currentPage - 1) * pageSize).Take(pageSize).Include(l => l.BenhNhan).ToListAsync();

                var vm = new LichHenListViewModel
                {
                    Appointments = myAppointments,
                    PageIndex = currentPage,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };

                return View(vm);
            }
            
            /*
            var appointments = await query.Where(l => l.BenhNhanId == benhNhan.Id).OrderBy(b => b.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize).Include(l => l.BenhNhan).Include(l => l.BacSi).Include(l => l.LeTan).ToListAsync();

            var vm = new LichHenListViewModel
            {
                Appointments = appointments,
                PageIndex = currentPage,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return View(vm);*/
        }

        // GET: LichHen/Details/5
        [Authorize(Roles = "Admin, Patient, Receptionist")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHen = await _context.LichHen
                .Include(l => l.BenhNhan)
                .Include(l => l.BacSi)
                .Include(l => l.LeTan)               
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichHen == null)
            {
                return NotFound();
            }

            return View(lichHen);
        }

        // GET: LichHen/Create
        [Authorize(Roles = "Patient, Receptionist")]
        public IActionResult Create()
        {
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhan.ToList(), "Id", "HoTen");
            ViewData["BacSiId"] = new SelectList(_context.Bacsi.ToList(), "Id", "HoTen");
            ViewData["LeTanId"] = new SelectList(_context.LeTan.ToList(), "Id", "HoTen");
            return View();
        }

        // POST: LichHen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient, Receptionist")]
        public async Task<IActionResult> Create([Bind("Id,BenhNhanId,BacSiId,LeTanId,NgayGio,TrangThai")] LichHen lichHen)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                ViewData["BenhNhanId"] = new SelectList(_context.BenhNhan, "Id", "HoTen", lichHen.BenhNhanId);
                ViewData["BacSiId"] = new SelectList(_context.Bacsi, "Id", "HoTen", lichHen.BacSiId);
                ViewData["LeTanId"] = new SelectList(_context.LeTan, "Id", "HoTen", lichHen.LeTanId);

                //Take User id logged in
                var userId = _userManager.GetUserId(User);
                var benhNhan = await _context.BenhNhan
                    .FirstOrDefaultAsync(b => b.UserId == userId);

                if (benhNhan == null)
                {
                    ModelState.AddModelError("", "Không tìm thấy bệnh nhân");
                    return View(lichHen);
                }
                lichHen.BenhNhanId = benhNhan.Id;
                lichHen.TrangThai = 0;

                //Check if patient book after working hours: 8:00 - 18:00
                var startWork = new TimeSpan(8, 0, 0);
                var endWork = new TimeSpan(18, 0, 0);

                //Return TimeSpan object, represent time elapsed
                var appointmentTime = lichHen.NgayGio.TimeOfDay;

                if (appointmentTime < startWork || appointmentTime >= endWork)
                {
                    ModelState.AddModelError("", "Giờ hẹn phải trong giờ làm việc từ 08:00 đến 18:00");
                    return View(lichHen);
                }

                //Check if doctor had appointment: 1 slot = 30 mins
                var startTime = lichHen.NgayGio;
                var endTime = lichHen.NgayGio.AddMinutes(30);

                var conflict = await _context.LichHen.AnyAsync(l => l.BacSiId == lichHen.BacSiId && 
                    (
                        //Appointment starts at existing time
                        (startTime >= l.NgayGio && startTime < l.NgayGio.AddMinutes(30)) ||

                        //Appointment ends at existing time
                        (endTime > l.NgayGio && endTime <= l.NgayGio.AddMinutes(30)) ||

                        //Appointment covers existing time
                        (startTime <= l.NgayGio && endTime >= l.NgayGio.AddMinutes(30))
                    )
                );
                
                if (conflict)
                {
                    ModelState.AddModelError("", "Bác sĩ đã có lịch ở thời điểm này vui lòng chọn giờ khác");
                    return View(lichHen);
                }

                _context.Add(lichHen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lichHen);
        }

        // GET: LichHen/Edit/5
        [Authorize(Roles = "Patient, Receptionist")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhan.ToList(), "Id", "HoTen");
            ViewData["BacSiId"] = new SelectList(_context.Bacsi.ToList(), "Id", "HoTen");
            ViewData["LeTanId"] = new SelectList(_context.LeTan.ToList(), "Id", "HoTen");
            if (id == null)
            {
                return NotFound();
            }

            var lichHen = await _context.LichHen.FindAsync(id);
            if (lichHen == null)
            {
                return NotFound();
            }
            
            return View(lichHen);
        }

        // POST: LichHen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Patient, Receptionist")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BenhNhanId,BacSiId,LeTanId,NgayGio,TrangThai")] LichHen lichHen)
        {
            if (id != lichHen.Id)
            {
                return NotFound();
            }

            ModelState.Clear();
            ViewData["BenhNhanId"] = new SelectList(_context.BenhNhan.ToList(), "Id", "HoTen");
            ViewData["BacSiId"] = new SelectList(_context.Bacsi.ToList(), "Id", "HoTen");
            ViewData["LeTanId"] = new SelectList(_context.LeTan.ToList(), "Id", "HoTen");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lichHen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LichHenExists(lichHen.Id))
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
            return View(lichHen);
        }

        // GET: LichHen/Delete/5
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lichHen = await _context.LichHen
                .Include(l => l.BenhNhan)
                .Include(l => l.BacSi)
                .Include(l => l.LeTan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lichHen == null)
            {
                return NotFound();
            }

            return View(lichHen);
        }

        // POST: LichHen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Receptionist")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lichHen = await _context.LichHen.FindAsync(id);
            if (lichHen != null)
            {
                _context.LichHen.Remove(lichHen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LichHenExists(int id)
        {
            return _context.LichHen.Any(e => e.Id == id);
        }
    }
}
