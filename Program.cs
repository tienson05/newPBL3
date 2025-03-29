using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Repository;
using HeThongMoiGioiDoCu.Services;

var builder = WebApplication.CreateBuilder(args);

//// Đăng ký dịch vụ UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Đăng ký AccountService vào DI container
builder.Services.AddScoped<AccountService>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
