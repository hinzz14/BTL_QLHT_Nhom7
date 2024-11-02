using AdminWebpage.Models;
using AdminWebpage.Repository;
using AdminWebPage.Interfaces;
using AdminWebPage.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // Thêm namespace này

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuanLyHieuThuocWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLHTContext")));
builder.Services.AddScoped<ILoaiThuocRepository, LoaiThuocRepository>();
builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian chờ 30 phút
    options.Cookie.HttpOnly = true; // Cookie chỉ có thể được truy cập qua HTTP
});

// Cấu hình xác thực
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login"; // Đường dẫn tới trang đăng nhập
        options.LogoutPath = "/Login/Logout"; // Đường dẫn tới trang đăng xuất
        options.AccessDeniedPath = "/Account/AccessDenied"; // Đường dẫn nếu không có quyền truy cập
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

// Sử dụng xác thực
app.UseAuthentication(); // Đảm bảo middleware xác thực được gọi
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Shopping}/{action=Login}/{id?}");

app.Run();
