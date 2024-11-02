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
    public class HoaDonBanController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public HoaDonBanController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        // GET: HoaDonBan
        public async Task<IActionResult> HDB(String Searchkey)
        {
            var quanLyHieuThuocWebContext = _context.THoaDonBans.Include(t => t.MaKhNavigation).Include(t => t.MaNvNavigation);
            if (!string.IsNullOrEmpty(Searchkey))
            {
                quanLyHieuThuocWebContext = quanLyHieuThuocWebContext.Where(nv => nv.SoHdb.Contains(Searchkey)).Include(t => t.MaKhNavigation).Include(t => t.MaNvNavigation);
            return View(await quanLyHieuThuocWebContext.ToListAsync());
            }
                return View(await quanLyHieuThuocWebContext.ToListAsync());
        }

        // GET: HoaDonBan/Details/5
        public async Task<IActionResult> DetailsHDB(string id)
        {
            if (id == null || _context.THoaDonBans == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans
                .Include(t => t.MaKhNavigation)
                .Include(t => t.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.SoHdb == id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }

            return View(tHoaDonBan);
        }

        // GET: HoaDonBan/Create
        public IActionResult AddHDB()
        {
            ViewBag.MaKh = new SelectList(_context.TKhachHangs, "MaKh", "MaKh");
            ViewBag.MaNv = new SelectList(_context.TNhanViens, "MaNv", "MaNv");
            return View();
        }

        // POST: HoaDonBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddHDB(THoaDonBan tHoaDonBan)
        {
            if (ModelState.IsValid)
            {
                _context.THoaDonBans.Add(tHoaDonBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HDB));
            }
            ViewBag.MaKh = new SelectList(_context.TKhachHangs, "MaKh", "MaKh", tHoaDonBan.MaKh);
            ViewBag.MaNv = new SelectList(_context.TNhanViens, "MaNv", "MaNv", tHoaDonBan.MaNv);
            return View(tHoaDonBan);
        }

        // GET: HoaDonBan/Edit/5
        public async Task<IActionResult> EditHDB(string id)
        {
            if (id == null || _context.THoaDonBans == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans.FindAsync(id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }
            ViewBag.MaKh = new SelectList(_context.TKhachHangs, "MaKh", "MaKh", tHoaDonBan.MaKh);
            ViewBag.MaNv = new SelectList(_context.TNhanViens, "MaNv", "MaNv", tHoaDonBan.MaNv);
            return View(tHoaDonBan);
        }

        // POST: HoaDonBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHDB(string id, [Bind("SoHdb,MaNv,NgayLap,MaKh,TongTien")] THoaDonBan tHoaDonBan)
        {
            if (id != tHoaDonBan.SoHdb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.THoaDonBans.Update(tHoaDonBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!THoaDonBanExists(tHoaDonBan.SoHdb))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(HDB));
            }
            ViewBag.MaKh = new SelectList(_context.TKhachHangs, "MaKh", "MaKh", tHoaDonBan.MaKh);
            ViewBag.MaNv = new SelectList(_context.TNhanViens, "MaNv", "MaNv", tHoaDonBan.MaNv);
            return View(tHoaDonBan);
        }

        // GET: HoaDonBan/Delete/5
        public async Task<IActionResult> DeleteHDB(string id)
        {
            if (id == null || _context.THoaDonBans == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans
                .Include(t => t.MaKhNavigation)
                .Include(t => t.MaNvNavigation)
                .FirstOrDefaultAsync(m => m.SoHdb == id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }

            return View(tHoaDonBan);
        }

        // POST: HoaDonBan/Delete/5
        [HttpPost, ActionName("DeleteHDB")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.THoaDonBans == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.THoaDonBans'  is null.");
            }
            var tHoaDonBan = await _context.THoaDonBans.FindAsync(id);
            if (tHoaDonBan != null)
            {
                _context.THoaDonBans.Remove(tHoaDonBan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(HDB));
        }

        private bool THoaDonBanExists(string id)
        {
          return (_context.THoaDonBans?.Any(e => e.SoHdb == id)).GetValueOrDefault();
        }
    }
}
