using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminWebpage.Models;
using System.Diagnostics;

namespace AdminWebpage.Controllers
{
    public class ChiTietHdnController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public ChiTietHdnController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        // GET: ChiTietHdn
        public async Task<IActionResult> CTHDN(string Searchkey, string id)
        {
            var quanLyHieuThuocWebContext = _context.TChiTietHdns.Include(t => t.MaThuocNavigation).Include(t => t.SoHdnNavigation);
            if (!string.IsNullOrEmpty(Searchkey))
            {
                quanLyHieuThuocWebContext = quanLyHieuThuocWebContext.Where(nv => nv.SoHdn.Contains(Searchkey)).Include(t => t.MaThuocNavigation).Include(t => t.SoHdnNavigation);
                return View(await quanLyHieuThuocWebContext.ToListAsync());
            }
            if (!string.IsNullOrEmpty(id))
            {
                quanLyHieuThuocWebContext = quanLyHieuThuocWebContext.Where(nv => nv.SoHdn.Contains(id)).Include(t => t.MaThuocNavigation).Include(t => t.SoHdnNavigation);
                ViewBag.HDn = id;
                return View(await quanLyHieuThuocWebContext.ToListAsync());
            }
            return View(await quanLyHieuThuocWebContext.ToListAsync());
        }

        // GET: ChiTietHdn/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TChiTietHdns == null)
            {
                return NotFound();
            }

            var tChiTietHdn = await _context.TChiTietHdns
                .Include(t => t.MaThuocNavigation)
                .Include(t => t.SoHdnNavigation)
                .FirstOrDefaultAsync(m => m.SoHdn == id);
            if (tChiTietHdn == null)
            {
                return NotFound();
            }

            return View(tChiTietHdn);
        }

        // GET: ChiTietHdn/Create
        public IActionResult AddCTHDN(string id)
        {
            ViewBag.MaThuoc = new SelectList(_context.TThuocs, "MaThuoc", "MaThuoc");
            ViewBag.SoHdn = new SelectList(_context.THoaDonNhaps, "SoHdn", "SoHdn");
            ViewBag.HD = id;
            return View();
        }

        // POST: ChiTietHdn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCTHDN([Bind("SoHdn,MaThuoc,Slnhap,KhuyenMai,ThanhTien")] TChiTietHdn tChiTietHdn)
        {
            if (ModelState.IsValid)
            {
                _context.TChiTietHdns.Add(tChiTietHdn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CTHDN));
            }
            ViewBag.MaThuoc = new SelectList(_context.TThuocs, "MaThuoc", "MaThuoc", tChiTietHdn.MaThuoc);
            ViewBag.SoHdn = new SelectList(_context.THoaDonNhaps, "SoHdn", "SoHdn", tChiTietHdn.SoHdn);
            return View(tChiTietHdn);
        }

        // GET: ChiTietHdn/Edit/5
        public async Task<IActionResult> EditCTHDN(string id, string mt)
        {
            if (id == null || mt == null || _context.TChiTietHdns == null)
            {
                return NotFound();
            }

            var tChiTietHdn = await _context.TChiTietHdns.FindAsync(id, mt);
            if (tChiTietHdn == null)
            {
                return NotFound();
            }
            ViewBag.MaThuoc = new SelectList(_context.TThuocs, "MaThuoc", "MaThuoc", tChiTietHdn.MaThuoc);
            ViewBag.SoHdn = new SelectList(_context.THoaDonNhaps, "SoHdn", "SoHdn", tChiTietHdn.SoHdn);
            return View(tChiTietHdn);
        }

        // POST: ChiTietHdn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCTHDN(string id,[Bind("SoHdn,MaThuoc,KhuyenMai,Slnhap,ThanhTien")] TChiTietHdn tChiTietHdn)
        {
            if (id != tChiTietHdn.SoHdn )
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TChiTietHdns.Update(tChiTietHdn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TChiTietHdnExists(tChiTietHdn.SoHdn))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CTHDN));
            }
            ViewBag.MaThuoc = new SelectList(_context.TThuocs, "MaThuoc", "MaThuoc", tChiTietHdn.MaThuoc);
            ViewBag.SoHdn = new SelectList(_context.THoaDonNhaps, "SoHdn", "SoHdn", tChiTietHdn.SoHdn);
            return View(tChiTietHdn);
        }

       
        public async Task<IActionResult> DeleteCTHDN(string id, string mt)
        {
            if (_context.TChiTietHdbs == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TChiTietHdbs' is null.");
            }
            if (id == null || mt == null || _context.TChiTietHdns == null)
            {
                return NotFound();
            }

            var tChiTietHdn = await _context.TChiTietHdns
                .Include(t => t.MaThuocNavigation)
                .Include(t => t.SoHdnNavigation)
                .FirstOrDefaultAsync(m => m.SoHdn == id && m.MaThuoc == mt);
            if (tChiTietHdn == null)
            {
                return NotFound();
            }

            _context.TChiTietHdns.Remove(tChiTietHdn);
            await _context.SaveChangesAsync();
            return RedirectToAction("HDN", "HoaDonNhap");
        }
        private bool TChiTietHdnExists(string id)
        {
            return (_context.TChiTietHdns?.Any(e => e.SoHdn == id)).GetValueOrDefault();
        }

    }
}
