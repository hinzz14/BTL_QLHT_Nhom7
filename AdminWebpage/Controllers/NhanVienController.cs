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
    public class NhanVienController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public NhanVienController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        // GET: NhanVien
        //public async Task<IActionResult> Staff(String Searchkey)
        //{
        //      return _context.TNhanViens != null ? 
        //                  View(await _context.TNhanViens.ToListAsync()) :
        //                  Problem("Entity set 'QuanLyHieuThuocWebContext.TNhanViens'  is null.");
        //}
        public async Task<IActionResult> Staff(String Searchkey)
        {
            // Check if the TNhanViens entity set is null.
            if (_context.TNhanViens == null)
            {
                // Return a problem response if the TNhanViens entity set is null.
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TNhanViens'  is null.");
            }
            
            else
            {
                if(!string.IsNullOrEmpty(Searchkey))
                {
                    var nhanvien = _context.TNhanViens.Where(nv => nv.TenNv.Contains(Searchkey));
                    return View(await nhanvien.ToListAsync());
                }
                else
                {
                    return View(await _context.TNhanViens.ToListAsync());
                }
                // Return the view if the TNhanViens entity set is not null.
                
            }
        }


        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TNhanViens == null)
            {
                return NotFound();
            }

            var tNhanVien = await _context.TNhanViens
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (tNhanVien == null)
            {
                return NotFound();
            }

            return View(tNhanVien);
        }

        // GET: NhanVien/Create
        public IActionResult AddStaff()
        {
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStaff([Bind("MaNv,TenNv,GioiTinh,NgaySinh,DiaChi,Sdt")] TNhanVien tNhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.TNhanViens.Add(tNhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Staff));
            }
            return View(tNhanVien);
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> EditStaff(string id)
        {
            if (id == null || _context.TNhanViens == null)
            {
                return NotFound();
            }

            var tNhanVien = await _context.TNhanViens.FindAsync(id);
            if (tNhanVien == null)
            {
                return NotFound();
            }
            return View(tNhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStaff(string id, [Bind("MaNv,TenNv,GioiTinh,NgaySinh,DiaChi,Sdt")] TNhanVien tNhanVien)
        {
            if (id != tNhanVien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TNhanViens.Update(tNhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNhanVienExists(tNhanVien.MaNv))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Staff));
            }
            return View(tNhanVien);
        }

        // GET: NhanVien/Delete/5
        public async Task<IActionResult> DeleteStaff(string id)
        {
            if (id == null || _context.TNhanViens == null)
            {
                return NotFound();
            }

            var tNhanVien = await _context.TNhanViens
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (tNhanVien == null)
            {
                return NotFound();
            }

            return View(tNhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("DeleteStaff")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TNhanViens == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TNhanViens'  is null.");
            }
            var tNhanVien = await _context.TNhanViens.FindAsync(id);
            if (tNhanVien != null)
            {
                _context.TNhanViens.Remove(tNhanVien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Staff));
        }

        private bool TNhanVienExists(string id)
        {
          return (_context.TNhanViens?.Any(e => e.MaNv == id)).GetValueOrDefault();
        }
    }
}
