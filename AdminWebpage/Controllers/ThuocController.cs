using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminWebpage.Models;
using System.Net.Http.Headers;
using AdminWebPage.Interfaces;

namespace AdminWebpage.Controllers
{
    public class ThuocController : Controller
    {
        private readonly QuanLyHieuThuocWebContext _context;
        readonly IBufferedFileUploadService _bufferedFileUploadService;
        public ThuocController(QuanLyHieuThuocWebContext context, IBufferedFileUploadService bufferedFileUploadService)
        {
            _context = context;
            _bufferedFileUploadService = bufferedFileUploadService;
        }
        public IActionResult SearchByName(string mid)
        {
            var thuocs = _context.TThuocs.Include(t => t.MaLoaiNavigation).ToList();
            if (!string.IsNullOrEmpty(mid))
            {
                thuocs = thuocs.Where(l => l.TenThuoc.Contains(mid)).ToList();
            }
              
            return PartialView("LearnerTable", thuocs);
        }
        //public IActionResult SearchByName(string mid, int pageNumber = 1, int pageSize = 3)
        //{
        //    var thuocs = _context.TThuocs.Include(t => t.MaLoaiNavigation).ToList();
        //    if (!string.IsNullOrEmpty(mid))
        //    {
        //        thuocs = thuocs.Where(l => l.TenThuoc.Contains(mid)).ToList(); // Sử dụng Contains thay vì bằng
        //    }
        //    var pagedthuocs = thuocs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        //    return PartialView("LearnerTable", pagedthuocs);
        //}
        //public IActionResult SearchByName(string mid, int pageNumber = 1, int pageSize = 3)
        //{
        //    var thuocs = _context.TThuocs.Include(t => t.MaLoaiNavigation).ToList();
        //    if (!string.IsNullOrEmpty(mid))
        //    {
        //        thuocs = thuocs.Where(l => l.TenThuoc.Contains(mid)).ToList(); // Sử dụng Contains thay vì bằng
        //    }
        //    var pagedthuocs = thuocs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        //    int totalItemCount = thuocs.Count; // Số lượng phần tử tìm thấy
        //    ViewBag.TotalItemCount = totalItemCount;
        //    return PartialView("Index", pagedthuocs);
        //}







        // GET: Thuoc
        //public async Task<IActionResult> Product(String Searchkey)
        //{

        //    var quanLyHieuThuocWebContext = _context.TThuocs.Include(t => t.MaLoaiNavigation);
        //    return View(await quanLyHieuThuocWebContext.ToListAsync());
        //}

        public async Task<IActionResult> Product(string Searchkey)
        {
            var quanLyHieuThuocWebContext = _context.TThuocs.Include(t => t.MaLoaiNavigation);

            if (!string.IsNullOrEmpty(Searchkey))
            {
                quanLyHieuThuocWebContext = quanLyHieuThuocWebContext.Where(t => t.TenThuoc.Contains(Searchkey)).Include(t => t.MaLoaiNavigation);
                return View(await quanLyHieuThuocWebContext.ToListAsync());
            }


            return View(await quanLyHieuThuocWebContext.ToListAsync());
        }






        // GET: Thuoc/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TThuocs == null)
            {
                return NotFound();
            }

            var tThuoc = await _context.TThuocs
                .Include(t => t.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.MaThuoc == id);
            if (tThuoc == null)
            {
                return NotFound();
            }

            return View(tThuoc);
        }

        // GET: Thuoc/Create
        public IActionResult AddProduct()
        {
            ViewBag.MaLoai = new SelectList(_context.TLoaiThuocs, "MaLoai", "MaLoai");
            return View();
        }

        // POST: Thuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(IFormFile file, [Bind("MaThuoc,TenThuoc,ThanhPhan,NgaySx,NgayHh,MaLoai,DonGiaBan,DonGiaNhap,SoLuong,TrongLuong,Anh")] TThuoc tThuoc)
        {
            var upFile = await _bufferedFileUploadService.UploadFile(file);
            if (upFile)
            {
                tThuoc.Anh = file.FileName;
            }
            else
            {
                tThuoc.Anh = "ava.jpg";
            }
            if (ModelState.IsValid)
            {
                _context.TThuocs.Add(tThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Product));
            }
            ViewBag.MaLoai = new SelectList(_context.TLoaiThuocs, "MaLoai", "MaLoai", tThuoc.MaLoai);
            return View(tThuoc);
        }

        // GET: Thuoc/Edit/5
        public async Task<IActionResult> EditProduct(string id)
        {
            if (id == null || _context.TThuocs == null)
            {
                return NotFound();
            }

            var tThuoc = await _context.TThuocs.FindAsync(id);
            if (tThuoc == null)
            {
                return NotFound();
            }
            ViewBag.MaLoai = new SelectList(_context.TLoaiThuocs, "MaLoai", "MaLoai", tThuoc.MaLoai);
            return View(tThuoc);
        }

        // POST: Thuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(IFormFile file, string id, [Bind("MaThuoc,TenThuoc,ThanhPhan,NgaySx,NgayHh,MaLoai,DonGiaBan,DonGiaNhap,SoLuong,TrongLuong,Anh")] TThuoc tThuoc)
        {
            var upFile = await _bufferedFileUploadService.UploadFile(file);
            if (upFile)
            {
                tThuoc.Anh = file.FileName;
            }
            else
            {
                tThuoc.Anh = "ava.jpg";
            }
            if (id != tThuoc.MaThuoc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TThuocs.Update(tThuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TThuocExists(tThuoc.MaThuoc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Product));
            }
            ViewBag.MaLoai = new SelectList(_context.TLoaiThuocs, "MaLoai", "MaLoai", tThuoc.MaLoai);
            return View(tThuoc);
        }

        // GET: Thuoc/Delete/5
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (id == null || _context.TThuocs == null)
            {
                return NotFound();
            }

            var tThuoc = await _context.TThuocs
                .Include(t => t.MaLoaiNavigation)
                .FirstOrDefaultAsync(m => m.MaThuoc == id);
            if (tThuoc == null)
            {
                return NotFound();
            }

            return View(tThuoc);
        }

        // POST: Thuoc/Delete/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TThuocs == null)
            {
                return Problem("Entity set 'QuanLyHieuThuocWebContext.TThuocs'  is null.");
            }
            var tThuoc = await _context.TThuocs.FindAsync(id);
            if (tThuoc != null)
            {
                _context.TThuocs.Remove(tThuoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Product));
        }

        private bool TThuocExists(string id)
        {
          return (_context.TThuocs?.Any(e => e.MaThuoc == id)).GetValueOrDefault();
        }
    }
}
