using MedicalSupplies.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm các dịch vụ (Services) vào DI Container
builder.Services.AddControllersWithViews();

// Đăng ký SupplyService dạng Singleton để giữ toàn vẹn danh sách dữ liệu mẫu trong bộ nhớ (In-memory List)
builder.Services.AddSingleton<SupplyService>();

var app = builder.Build();

// 2. Cấu hình HTTP request pipeline (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Giá trị HSTS mặc định là 30 ngày. Bạn có thể thay đổi khi triển khai thực tế.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Kích hoạt Middleware xử lý các file tĩnh (như CSS, JS, hình ảnh) được tối ưu hóa trong .NET 9
app.MapStaticAssets();

app.UseRouting();

app.UseAuthorization();

// 3. Thiết lập MVC routing định tuyến URL
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets(); // Tích hợp tối ưu hóa tài nguyên tĩnh với hệ thống route mặc định

app.Run();