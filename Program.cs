using Microsoft.EntityFrameworkCore; // Thêm namespace này
using Asp.Net_MvcWeb_Pj3.Aptech.Models; // Thêm dòng này
using System.IO;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký DataContext vào DI container
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite("Data Source=.data/data.db")); // Đường dẫn tới file data.db

var app = builder.Build();

// Tạo và cập nhật cơ sở dữ liệu khi ứng dụng khởi động
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dbContext.Database.EnsureCreated(); // Tạo cơ sở dữ liệu nếu chưa tồn tại
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
