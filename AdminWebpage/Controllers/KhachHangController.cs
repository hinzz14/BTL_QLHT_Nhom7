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
    public class KhachHangController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public KhachHangController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        // GET: KhachHang
        public async Task<IActionResult> Client(String Searchkey)
        {
            if (_context.TKhachHangs == null)
            {
                // Return a problem response if the TNhanViens entity set is null.
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TNhanViens'  is null.");
            }

            else
            {
                if (!string.IsNullOrEmpty(Searchkey))
                {
                    var nhanvien = _context.TKhachHangs.Where(nv => nv.TenKh.Contains(Searchkey));
                    return View(await nhanvien.ToListAsync());
                }
                else
                {
                    return View(await _context.TKhachHangs.ToListAsync());
                }
                // Return the view if the TNhanViens entity set is not null.

            }
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TKhachHangs == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (tKhachHang == null)
            {
                return NotFound();
            }

            return View(tKhachHang);
        }

        // GET: KhachHang/Create
        public IActionResult AddClient()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClient([Bind("MaKh,TenKh,GioiTinh,NgaySinh,DiaChi,Sdt")] TKhachHang tKhachHang)
        {
            if (ModelState.IsValid)
            {
                _context.TKhachHangs.Add(tKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Client));
            }
            return View(tKhachHang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> EditClient(string id)
        {
            if (id == null || _context.TKhachHangs == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs.FindAsync(id);
            if (tKhachHang == null)
            {
                return NotFound();
            }
            return View(tKhachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClient(string id, [Bind("MaKh,TenKh,GioiTinh,NgaySinh,DiaChi,Sdt")] TKhachHang tKhachHang)
        {
            if (id != tKhachHang.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TKhachHangs.Update(tKhachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TKhachHangExists(tKhachHang.MaKh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Client));
            }
            return View(tKhachHang);
        }

        // GET: KhachHang/Delete/5
        public async Task<IActionResult> DeleteClient(string id)
        {
            if (id == null || _context.TKhachHangs == null)
            {
                return NotFound();
            }

            var tKhachHang = await _context.TKhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (tKhachHang == null)
            {
                return NotFound();
            }

            return View(tKhachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("DeleteClient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TKhachHangs == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TKhachHangs'  is null.");
            }
            var tKhachHang = await _context.TKhachHangs.FindAsync(id);
            if (tKhachHang != null)
            {
                _context.TKhachHangs.Remove(tKhachHang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Client));
        }

        private bool TKhachHangExists(string id)
        {
          return (_context.TKhachHangs?.Any(e => e.MaKh == id)).GetValueOrDefault();
        }
    }
}
