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
    public class NhaCungCapController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public NhaCungCapController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        // GET: NhaCungCap
        public async Task<IActionResult> Supplier(String Searchkey)
        {
            if (_context.TNhaCungCaps == null)
            {
                // Return a problem response if the TNhanViens entity set is null.
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TNhanViens'  is null.");
            }

            else
            {
                if (!string.IsNullOrEmpty(Searchkey))
                {
                    var nhanvien = _context.TNhaCungCaps.Where(nv => nv.TenNcc.Contains(Searchkey));
                    return View(await nhanvien.ToListAsync());
                }
                else
                {
                    return View(await _context.TNhaCungCaps.ToListAsync());
                }
                // Return the view if the TNhanViens entity set is not null.

            }
        }

        // GET: NhaCungCap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TNhaCungCaps == null)
            {
                return NotFound();
            }

            var tNhaCungCap = await _context.TNhaCungCaps
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (tNhaCungCap == null)
            {
                return NotFound();
            }

            return View(tNhaCungCap);
        }

        // GET: NhaCungCap/Create
        public IActionResult AddSupplier()
        {
            return View();
        }

        // POST: NhaCungCap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSupplier([Bind("MaNcc,TenNcc,Sdt,Email,DiaChi")] TNhaCungCap tNhaCungCap)
        {
            if (ModelState.IsValid)
            {
                _context.TNhaCungCaps.Add(tNhaCungCap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Supplier));
            }
            return View(tNhaCungCap);
        }

        // GET: NhaCungCap/Edit/5
        public async Task<IActionResult> EditSupplier(string id)
        {
            if (id == null || _context.TNhaCungCaps == null)
            {
                return NotFound();
            }

            var tNhaCungCap = await _context.TNhaCungCaps.FindAsync(id);
            if (tNhaCungCap == null)
            {
                return NotFound();
            }
            return View(tNhaCungCap);
        }

        // POST: NhaCungCap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSupplier(string id, [Bind("MaNcc,TenNcc,Sdt,Email,DiaChi")] TNhaCungCap tNhaCungCap)
        {
            if (id != tNhaCungCap.MaNcc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TNhaCungCaps.Update(tNhaCungCap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNhaCungCapExists(tNhaCungCap.MaNcc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Supplier));
            }
            return View(tNhaCungCap);
        }

        // GET: NhaCungCap/Delete/5
        public async Task<IActionResult> DeleteSupplier(string id)
        {
            if (id == null || _context.TNhaCungCaps == null)
            {
                return NotFound();
            }

            var tNhaCungCap = await _context.TNhaCungCaps
                .FirstOrDefaultAsync(m => m.MaNcc == id);
            if (tNhaCungCap == null)
            {
                return NotFound();
            }

            return View(tNhaCungCap);
        }

        // POST: NhaCungCap/Delete/5
        [HttpPost, ActionName("DeleteSupplier")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TNhaCungCaps == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TNhaCungCaps'  is null.");
            }
            var tNhaCungCap = await _context.TNhaCungCaps.FindAsync(id);
            if (tNhaCungCap != null)
            {
                _context.TNhaCungCaps.Remove(tNhaCungCap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Supplier));
        }

        private bool TNhaCungCapExists(string id)
        {
          return (_context.TNhaCungCaps?.Any(e => e.MaNcc == id)).GetValueOrDefault();
        }
    }
}
