using AdminWebpage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace AdminWebpage.Controllers
{
   
    public class LoginController : Controller
    {
        QuanLyHieuThuocWebContext db = new QuanLyHieuThuocWebContext();

        [HttpGet]
       
        public IActionResult Login()
        {

            return View();
        }
        public IActionResult Index()
        {
            // Kiểm tra người dùng đã đăng nhập hay chưa
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Login");
            }

            // Nếu đã đăng nhập, trả về trang Shopping
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Account", "Password")] Login ad)
        {
            var _user = db.Logins.Where(m => m.Account == ad.Account && m.Password == ad.Password).FirstOrDefault();            
            if (_user == null)
            {
                ViewBag.LoginStatus = 0;
                return View();
            }
            else
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.Account),
                new Claim("FullName", _user.Account),
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                };

                await HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   authProperties);

                var isAdmin = db.Logins.FirstOrDefault(m => m.Role == "Admin" && m.Account == ad.Account && m.Password == ad.Password);

                if (isAdmin != null)
                {
                    return RedirectToAction("Index", "Home"); // Admin là 1 controller HomeIndex
                }
                else
                {
                    return RedirectToAction("Index", "Shopping");
                }
            }
            
        }

        public async Task<IActionResult> Logout()
        {
            // Xóa tất cả dữ liệu trong phiên
            HttpContext.Session.Clear();

            // Đăng xuất người dùng
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Chuyển hướng người dùng về trang đăng nhập
            return RedirectToAction("Index", "Shopping");
        }

    }
}
