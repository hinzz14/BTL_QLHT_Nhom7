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
    public class LoaiThuocController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public LoaiThuocController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        // GET: LoaiThuoc
        public async Task<IActionResult> Index()
        {
              return _context.TLoaiThuocs != null ? 
                          View(await _context.TLoaiThuocs.ToListAsync()) :
                          Problem("Entity set 'QuanLyHieuThuocWebContext.TLoaiThuocs'  is null.");
        }

        // GET: LoaiThuoc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TLoaiThuocs == null)
            {
                return NotFound();
            }

            var tLoaiThuoc = await _context.TLoaiThuocs
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (tLoaiThuoc == null)
            {
                return NotFound();
            }

            return View(tLoaiThuoc);
        }

        // GET: LoaiThuoc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiThuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenLoai")] TLoaiThuoc tLoaiThuoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tLoaiThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tLoaiThuoc);
        }

        // GET: LoaiThuoc/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TLoaiThuocs == null)
            {
                return NotFound();
            }

            var tLoaiThuoc = await _context.TLoaiThuocs.FindAsync(id);
            if (tLoaiThuoc == null)
            {
                return NotFound();
            }
            return View(tLoaiThuoc);
        }

        // POST: LoaiThuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaLoai,TenLoai")] TLoaiThuoc tLoaiThuoc)
        {
            if (id != tLoaiThuoc.MaLoai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tLoaiThuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TLoaiThuocExists(tLoaiThuoc.MaLoai))
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
            return View(tLoaiThuoc);
        }

        // GET: LoaiThuoc/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TLoaiThuocs == null)
            {
                return NotFound();
            }

            var tLoaiThuoc = await _context.TLoaiThuocs
                .FirstOrDefaultAsync(m => m.MaLoai == id);
            if (tLoaiThuoc == null)
            {
                return NotFound();
            }

            return View(tLoaiThuoc);
        }

        // POST: LoaiThuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TLoaiThuocs == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TLoaiThuocs'  is null.");
            }
            var tLoaiThuoc = await _context.TLoaiThuocs.FindAsync(id);
            if (tLoaiThuoc != null)
            {
                _context.TLoaiThuocs.Remove(tLoaiThuoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TLoaiThuocExists(string id)
        {
          return (_context.TLoaiThuocs?.Any(e => e.MaLoai == id)).GetValueOrDefault();
        }
    }
}
