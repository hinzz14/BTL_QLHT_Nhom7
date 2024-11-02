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
    public class ChiTietHdbController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public ChiTietHdbController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        // GET: ChiTietHdb
        public async Task<IActionResult> CTHDB(string Searchkey, string id)
        {
            var quanLyHieuThuocWebContext = _context.TChiTietHdbs.Include(t => t.MaThuocNavigation).Include(t => t.SoHdbNavigation);
            
            if (!string.IsNullOrEmpty(Searchkey))
            {
                quanLyHieuThuocWebContext = quanLyHieuThuocWebContext.Where(nv => nv.SoHdb.Contains(Searchkey)).Include(t => t.MaThuocNavigation).Include(t => t.SoHdbNavigation);
                return View(await quanLyHieuThuocWebContext.ToListAsync());
            }
            if (!string.IsNullOrEmpty(id))
            { 
                quanLyHieuThuocWebContext = quanLyHieuThuocWebContext.Where(nv => nv.SoHdb.Contains(id)).Include(t => t.MaThuocNavigation).Include(t => t.SoHdbNavigation);
                ViewBag.HDb = id;
                return View(await quanLyHieuThuocWebContext.ToListAsync());

            }
            return View(await quanLyHieuThuocWebContext.ToListAsync());
        }

        // GET: ChiTietHdb/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TChiTietHdbs == null)
            {
                return NotFound();
            }

            var tChiTietHdb = await _context.TChiTietHdbs
                .Include(t => t.MaThuocNavigation)
                .Include(t => t.SoHdbNavigation)
                .FirstOrDefaultAsync(m => m.SoHdb == id);
            if (tChiTietHdb == null)
            {
                return NotFound();
            }

            return View(tChiTietHdb);
        }

        // GET: ChiTietHdb/Create
        public IActionResult AddCTHDB(string id)
        {
            ViewBag.MaThuoc = new SelectList(_context.TThuocs, "MaThuoc", "MaThuoc");
            ViewBag.SoHdb = new SelectList(_context.THoaDonBans, "SoHdb", "SoHdb");
            ViewBag.HD = id;
            return View();
        }

        // POST: ChiTietHdb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCTHDB([Bind("SoHdb,MaThuoc,Slban,KhuyenMai,ThanhTien")] TChiTietHdb tChiTietHdb)
        {
            if (ModelState.IsValid)
            {
                _context.TChiTietHdbs.Add(tChiTietHdb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CTHDB));
            }
            ViewBag.MaThuoc = new SelectList(_context.TThuocs, "MaThuoc", "MaThuoc", tChiTietHdb.MaThuoc);
            ViewBag.SoHdb = new SelectList(_context.THoaDonBans, "SoHdb", "SoHdb", tChiTietHdb.SoHdb);
            return View(tChiTietHdb);
        }

        // GET: ChiTietHdb/Edit/5
        public async Task<IActionResult> EditCTHDB(string id, string mt)
        {
            if (id == null || mt == null || _context.TChiTietHdbs == null)
            {
                return NotFound();
            }

            var tChiTietHdb = await _context.TChiTietHdbs.FindAsync(id, mt);
            if (tChiTietHdb == null)
            {
                return NotFound();
            }
            ViewBag.MaThuoc = new SelectList(_context.TThuocs, "MaThuoc", "MaThuoc", tChiTietHdb.MaThuoc);
            ViewBag.SoHdb = new SelectList(_context.THoaDonBans, "SoHdb", "SoHdb", tChiTietHdb.SoHdb);
            return View(tChiTietHdb);
        }

        // POST: ChiTietHdb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCTHDB(string id, [Bind("SoHdb,MaThuoc,Slban,KhuyenMai,ThanhTien")] TChiTietHdb tChiTietHdb)
        {
            if (id != tChiTietHdb.SoHdb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TChiTietHdbs.Update(tChiTietHdb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TChiTietHdbExists(tChiTietHdb.SoHdb))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CTHDB));
            }
            ViewBag.MaThuoc = new SelectList(_context.TThuocs, "MaThuoc", "MaThuoc", tChiTietHdb.MaThuoc);
            ViewBag.SoHdb = new SelectList(_context.THoaDonBans, "SoHdb", "SoHdb", tChiTietHdb.SoHdb);
            return View(tChiTietHdb);
        }

        public async Task<IActionResult> DeleteCTHDB(string id, string mt)
        {
            if (_context.TChiTietHdbs == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TChiTietHdbs' is null.");
            }
            if (id == null || mt == null || _context.TChiTietHdbs == null)
            {
                return NotFound();
            }

            var tChiTietHdb = await _context.TChiTietHdbs
                .Include(t => t.MaThuocNavigation)
                .Include(t => t.SoHdbNavigation)
                .FirstOrDefaultAsync(m => m.SoHdb == id && m.MaThuoc == mt);
            if (tChiTietHdb == null)
            {
                return NotFound();
            }

            _context.TChiTietHdbs.Remove(tChiTietHdb);
            await _context.SaveChangesAsync();
            return RedirectToAction("HDB", "HoaDonBan");
        }



        private bool TChiTietHdbExists(string id)
        {
          return (_context.TChiTietHdbs?.Any(e => e.SoHdb == id)).GetValueOrDefault();
        }
    }
}
