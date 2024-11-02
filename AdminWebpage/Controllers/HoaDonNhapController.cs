using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminWebpage.Models;

namespace AdminWebpage.Controllers
{
    public class HoaDonNhapController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public HoaDonNhapController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        // GET: HoaDonNhap
        public async Task<IActionResult> HDN(String Searchkey)
        {
            var quanLyHieuThuocWebContext = _context.THoaDonNhaps.Include(t => t.MaNccNavigation).Include(t => t.MaNvNavigation);
            if (!string.IsNullOrEmpty(Searchkey))
            {
                quanLyHieuThuocWebContext = quanLyHieuThuocWebContext.Where(nv => nv.SoHdn.Contains(Searchkey)).Include(t => t.MaNccNavigation).Include(t => t.MaNvNavigation);
                return View(await quanLyHieuThuocWebContext.ToListAsync());
            }
                return View(await quanLyHieuThuocWebContext.ToListAsync());
        }

        // GET: HoaDonNhap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.THoaDonNhaps == null)
            {
                return NotFound();
            }

            var tHoaDonNhap = await _context.THoaDonNhaps
                .Include(t => t.MaNccNavigation)
                .Include(t => t.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.SoHdn == id);
            if (tHoaDonNhap == null)
            {
                return NotFound();
            }

            return View(tHoaDonNhap);
        }

        // GET: HoaDonNhap/Create
        public IActionResult AddHDN()
        {
            ViewBag.MaNcc = new SelectList(_context.TNhaCungCaps, "MaNcc", "MaNcc");
            ViewBag.MaNv = new SelectList(_context.TNhanViens, "MaNv", "MaNv");
            return View();
        }

        // POST: HoaDonNhap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHDN([Bind("SoHdn,MaNv,NgayLap,MaNcc,TongTien")] THoaDonNhap tHoaDonNhap)
        {
            if (ModelState.IsValid)
            {
                _context.THoaDonNhaps.Add(tHoaDonNhap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HDN));
            }
            ViewBag.MaNcc = new SelectList(_context.TNhaCungCaps, "MaNcc", "MaNcc", tHoaDonNhap.MaNcc);
            ViewBag.MaNv = new SelectList(_context.TNhanViens, "MaNv", "MaNv", tHoaDonNhap.MaNv);
            return View(tHoaDonNhap);
        }

        // GET: HoaDonNhap/Edit/5
        public async Task<IActionResult> EditHDN(string id)
        {
            if (id == null || _context.THoaDonNhaps == null)
            {
                return NotFound();
            }

            var tHoaDonNhap = await _context.THoaDonNhaps.FindAsync(id);
            if (tHoaDonNhap == null)
            {
                return NotFound();
            }
            ViewBag.MaNcc = new SelectList(_context.TNhaCungCaps, "MaNcc", "MaNcc", tHoaDonNhap.MaNcc);
            ViewBag.MaNv = new SelectList(_context.TNhanViens, "MaNv", "MaNv", tHoaDonNhap.MaNv);
            return View(tHoaDonNhap);
        }

        // POST: HoaDonNhap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHDN(string id, [Bind("SoHdn,MaNv,NgayLap,MaNcc,TongTien")] THoaDonNhap tHoaDonNhap)
        {
            if (id != tHoaDonNhap.SoHdn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.THoaDonNhaps.Update(tHoaDonNhap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!THoaDonNhapExists(tHoaDonNhap.SoHdn))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(HDN));
            }
            ViewBag.MaNcc = new SelectList(_context.TNhaCungCaps, "MaNcc", "MaNcc", tHoaDonNhap.MaNcc);
            ViewBag.MaNv = new SelectList(_context.TNhanViens, "MaNv", "MaNv", tHoaDonNhap.MaNv);
            return View(tHoaDonNhap);
        }

        // GET: HoaDonNhap/Delete/5
        public async Task<IActionResult> DeleteHDN(string id)
        {
            if (id == null || _context.THoaDonNhaps == null)
            {
                return NotFound();
            }

            var tHoaDonNhap = await _context.THoaDonNhaps
                .Include(t => t.MaNccNavigation)
                .Include(t => t.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.SoHdn == id);
            if (tHoaDonNhap == null)
            {
                return NotFound();
            }

            return View(tHoaDonNhap);
        }

        // POST: HoaDonNhap/Delete/5
        [HttpPost, ActionName("DeleteHDN")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.THoaDonNhaps == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.THoaDonNhaps'  is null.");
            }
            var tHoaDonNhap = await _context.THoaDonNhaps.FindAsync(id);
            if (tHoaDonNhap != null)
            {
                _context.THoaDonNhaps.Remove(tHoaDonNhap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(HDN));
        }

        private bool THoaDonNhapExists(string id)
        {
          return (_context.THoaDonNhaps?.Any(e => e.SoHdn == id)).GetValueOrDefault();
        }
    }
}
