using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HeThongMoiGioiDoCu.Services;
using HeThongMoiGioiDoCu.Repository.UserRepo;
using HeThongMoiGioiDoCu.Repository.ProductRepo;
using HeThongMoiGioiDoCu.Repository.CategoryRepo;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext với kết nối đến cơ sở dữ liệu
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký dịch vụ UserRepository và các dịch vụ khác
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Đăng ký AccountService vào DI container
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<JwtTokenProviderService>();

// Cấu hình JWT Bearer Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // Lấy giá trị từ file cấu hình (appsettings.json)
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
