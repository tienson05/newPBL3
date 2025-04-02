using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Repository;
using HeThongMoiGioiDoCu.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext với kết nối đến cơ sở dữ liệu
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký dịch vụ UserRepository và các dịch vụ khác
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Đăng ký AccountService vào DI container
builder.Services.AddScoped<AccountService>();

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500") // Địa chỉ của Frontend
              .AllowAnyHeader()   // Cho phép bất kỳ header
              .AllowAnyMethod();  // Cho phép bất kỳ phương thức HTTP (GET, POST, v.v.)
    });
});

// Thêm dịch vụ controllers (Web API)
builder.Services.AddControllers();

// Cấu hình Swagger/OpenAPI cho API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cấu hình đường dẫn yêu cầu HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Áp dụng CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
