using AdminWebpage.Infrastructure;
using AdminWebpage.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AdminWebpage.Controllers
{
    public class ShoppingController : Controller
    {
        // Cơ sở dữ liệu:
        private QuanLyHieuThuocWebContext db;
        public ShoppingController(QuanLyHieuThuocWebContext db)
        {
            this.db = db;
        }
       
        public IActionResult Index()
        {
            var drugCategories = db.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;

            List<TThuoc> drugs;
            if (db.TThuocs.Count() <= 8)
                drugs = db.TThuocs.ToList();
            else
                drugs = db.TThuocs.Take(8).ToList();
            ViewBag.Drugs = drugs;

            var thuocJoinLoaiThuoc = (from thuoc in db.TThuocs
                                      join loaithuoc in db.TLoaiThuocs
                                      on thuoc.MaLoai equals loaithuoc.MaLoai
                                      group thuoc by new
                                      {
                                          loaithuoc.MaLoai,
                                          loaithuoc.TenLoai,
                                      } into g
                                      select new TThuocJoinTLoaiThuoc
                                      {
                                          MaLoai = g.Key.MaLoai,
                                          TenLoai = g.Key.TenLoai,
                                          sumTheoLoai = g.Count()
                                      }).ToList();
            ViewBag.thuocJoinLoaiThuoc = thuocJoinLoaiThuoc;

            return View(thuocJoinLoaiThuoc);
        }

        public IActionResult Cart()
        {
            var drugCategories = db.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
        public IActionResult Login()
        {
           
            var drugCategories = db.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;

            List<TThuoc> drugs;
            if (db.TThuocs.Count() <= 8)
                drugs = db.TThuocs.ToList();
            else
                drugs = db.TThuocs.Take(8).ToList();
            ViewBag.Drugs = drugs;

            var thuocJoinLoaiThuoc = (from thuoc in db.TThuocs
                                      join loaithuoc in db.TLoaiThuocs
                                      on thuoc.MaLoai equals loaithuoc.MaLoai
                                      group thuoc by new
                                      {
                                          loaithuoc.MaLoai,
                                          loaithuoc.TenLoai,
                                      } into g
                                      select new TThuocJoinTLoaiThuoc
                                      {
                                          MaLoai = g.Key.MaLoai,
                                          TenLoai = g.Key.TenLoai,
                                          sumTheoLoai = g.Count()
                                      }).ToList();
            ViewBag.thuocJoinLoaiThuoc = thuocJoinLoaiThuoc;

            return View(thuocJoinLoaiThuoc);
        }
        public IActionResult Detail(string id)
        {
            var drugCategories = db.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;

            var drug = db.TThuocs.FirstOrDefault(m => m.MaThuoc == id);
            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Account, Password")] Login login)
        {
            login.Role = "User";
            db.Logins.Add(login);
            await db.SaveChangesAsync();
            return RedirectToAction("Login", "Login");    
        }

        public IActionResult Shop(string SearchItem, int pageNumber = 1, int pageSize = 6)
        {
            if (!string.IsNullOrEmpty(SearchItem))
            {
                var drugCategories1 = db.TLoaiThuocs.ToList();
                ViewBag.Categories = drugCategories1;

                var drugs1 = db.TThuocs.Where(t => t.TenThuoc.Contains(SearchItem))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                if (drugs1 == null)
                {
                    return NotFound();
                }

                int totalPages1 = (int)Math.Ceiling(db.TThuocs.Count() / (double)pageSize);

                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages1;
                return View(drugs1);
            }
            var drugCategories = db.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;

            var drugs = db.TThuocs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            if (drugs == null)
            {
                return NotFound();
            }

            int totalPages = (int)Math.Ceiling(db.TThuocs.Count() / (double)pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            return View(drugs);
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FilterAndPaginate(string filter, int pageNumber = 1, int pageSize = 6)
        {
            var query = db.TThuocs.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(t => t.TenThuoc.Contains(filter));
            }

            int totalPages = (int)Math.Ceiling(db.TThuocs.Count() / (double)pageSize);

            var drugs = query
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(totalPages)
                        .ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.FilterString = filter;
            return View(drugs);
        }
        

        public IActionResult DrugsByCategory(string category, string SearchItem)
        {
            var drugs = db.TThuocs
                        .Where(m => m.MaLoai == category)
                        .OrderBy(m => m.TenThuoc)
                        .ToList();
            if (!string.IsNullOrEmpty(SearchItem))
            {
                var drugs1 = db.TThuocs
                        .Where(m => m.TenThuoc == SearchItem)
                        .OrderBy(m => m.TenThuoc)
                        .ToList();
                ViewBag.Drugs = drugs1;
                return View(drugs1);
            }
            ViewBag.Drugs = drugs;
            return View(drugs);
        }
        public IActionResult ShopSorting(string SearchItem, string sortOrder, int pageNumber = 1, int pageSize = 6)
        {
            var drugCategories = db.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;

            var drugs = db.TThuocs.ToList();

            if (!string.IsNullOrEmpty(SearchItem))
            {
                drugs = drugs.Where(t => t.TenThuoc.Contains(SearchItem)).ToList();
            }

            switch (sortOrder)
            {
                case "asc":
                    drugs = drugs.OrderBy(t => t.DonGiaBan).ToList();
                    break;
                case "desc":
                    drugs = drugs.OrderByDescending(t => t.DonGiaBan).ToList();
                    break;
                default:
                    drugs = drugs.OrderBy(t => t.MaThuoc).ToList();
                    break;
            }

            int totalDrugs = drugs.Count();

            int totalPages = (int)Math.Ceiling(totalDrugs / (double)pageSize);

            drugs = drugs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalDrugs = totalDrugs;
            ViewBag.SearchItem = SearchItem;
            ViewBag.SortOrder = sortOrder;

            return View(drugs);
        }
    }
}
