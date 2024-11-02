using AdminWebpage.Infrastructure;
using AdminWebpage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using QRCoder;
using System.Drawing;

namespace AdminWebpage.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly QuanLyHieuThuocWebContext _context;

        public CartController(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    var drugCategories1 = _context.TThuocs.ToList();
        //    ViewBag.Categories = drugCategories1;
        //    Cart = HttpContext.Session.GetJson<Cart>("cart");
        //    if (Cart != null)
        //    {
        //        Cart.Clear();
        //        HttpContext.Session.SetJson("cart", Cart);
        //    }
        //    return View("AddToCart", HttpContext.Session.GetJson<Cart>("cart"));
        //}
        public IActionResult Index()
        {
            var drugCategories1 = _context.TThuocs.ToList();
            ViewBag.Categories = drugCategories1;
            Cart = HttpContext.Session.GetJson<Cart>("cart");

            if (Cart == null) // Kiểm tra nếu giỏ hàng là null, thì tạo một giỏ hàng mới
            {
                Cart = new Cart();
                HttpContext.Session.SetJson("cart", Cart);
            }

            return View("AddToCart", Cart);
        }
        public IActionResult ShoppingCart()
        {
            var drugCategories = _context.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;
            Cart = HttpContext.Session.GetJson<Cart>("cart");

            if (Cart == null) // Kiểm tra nếu giỏ hàng là null, thì tạo một giỏ hàng mới
            {
                Cart = new Cart();
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }

        public IActionResult CheckOut()
        {
            var drugCategories = _context.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories;

            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
        public IActionResult AddToCart(string? MaThuoc, int sl)
        {
            var drugCategories1 = _context.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories1;
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 256 // or any suitable value
            };
            try
            {
                // Do your serialization here
                var jsonString = JsonSerializer.Serialize(Cart, options);

                // Handle the JSON string as required
            }
            catch (JsonException ex)
            {
                // Handle the exception appropriately
            }
            //Kiểm tra (so sánh với csdl)
            TThuoc? thuoc = _context.TThuocs.FirstOrDefault(p => p.MaThuoc == MaThuoc);
           
           
            if (thuoc != null && sl != 1)
            {
                //Nếu đã có thì lấy chưa có thì tạo cart mới
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(thuoc, sl);
                HttpContext.Session.SetJson("cart", Cart);
            }
            if (thuoc != null && sl == 1)
            {
                //Nếu đã có thì lấy chưa có thì tạo cart mới
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(thuoc, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            //Truyền thông tin
            return View(Cart);
        }

        public IActionResult RemoveFromCart(string? MaThuoc)
        {
            var drugCategories1 = _context.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories1;
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 256 // or any suitable value
            };
            try
            {
                // Do your serialization here
                var jsonString = JsonSerializer.Serialize(Cart, options);

                // Handle the JSON string as required
            }
            catch (JsonException ex)
            {
                // Handle the exception appropriately
            }
            //Kiểm tra (so sánh với csdl)
            TThuoc? thuoc = _context.TThuocs.FirstOrDefault(p => p.MaThuoc == MaThuoc);
            if (thuoc != null)
            {
                //Nếu đã có thì xoas
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveLine(thuoc);
                HttpContext.Session.SetJson("cart", Cart);
            }
            //Truyền thông tin
            return View("AddToCart",Cart);
        }

        //Giảm số lượng sp
        public IActionResult UpdateCart(string? MaThuoc)
        {
            var drugCategories1 = _context.TLoaiThuocs.ToList();
            ViewBag.Categories = drugCategories1;
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 256 // or any suitable value
            };
            try
            {
                // Do your serialization here
                var jsonString = JsonSerializer.Serialize(Cart, options);

                // Handle the JSON string as required
            }
            catch (JsonException ex)
            {
                // Handle the exception appropriately
            }
            //Kiểm tra (so sánh với csdl)
            TThuoc? thuoc = _context.TThuocs.FirstOrDefault(p => p.MaThuoc == MaThuoc);
            if (thuoc != null)
            {
                //Nếu đã có thì lấy chưa có thì tạo cart mới
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(thuoc, -1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            //Truyền thông tin
            return View("AddToCart", Cart);
        }

        public IActionResult PlaceOrder()
        {
            // Logic to place the order
            TempData["SuccessMessage"] = "Order placed successfully!";
            return RedirectToAction("Payment");
        }

        public IActionResult Payment()
        {
            return View();
        }



    }
}

