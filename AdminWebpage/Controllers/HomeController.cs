using AdminWebpage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace AdminWebpage.Controllers
{
    public class HomeController : Controller
    {
  
        private readonly QuanLyHieuThuocWebContext _context;

        public HomeController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Thuoc = _context.TThuocs.Sum(h => h.SoLuong);
            ViewBag.TongMua = _context.TChiTietHdbs.Sum(h => h.ThanhTien);
            ViewBag.TongBan = _context.TChiTietHdns.Sum(h => h.ThanhTien);
            ViewBag.DoanhThu = _context.TChiTietHdbs.Sum(h => h.ThanhTien) - _context.TChiTietHdns.Sum(h => h.ThanhTien);
            
            
            return View();
        }


        public IActionResult Report()
        {
            return View();
        }
        public IActionResult Login()
        {
            var drugCategories = _context.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public IActionResult LoginToAdmin()
        //{
        //    // Kiểm tra phân quyền của người dùng
        //    if (HttpContext.Session["Role"] == "Admin")
        //    {
        //        // Người dùng có quyền truy cập
        //        return RedirectToAction("Index", "Home");
        //    }
            
        //        // Người dùng không có quyền truy cập
        //        return RedirectToAction("Index", "Shopping");    
        //}
    }
}