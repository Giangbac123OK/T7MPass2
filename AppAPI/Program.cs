using AppData;
using AppData.IRepository;
using AppData.IService;
using AppData.Repository;
using AppData.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IPhuongThucThanhToanRepo, PhuongThucThanhToanRepo>();
builder.Services.AddScoped<IPhuongThucThanhToanService, PhuongThucThanhToanService>();
builder.Services.AddScoped<IGiamGiaRepo, GiamGiaRepo>();
builder.Services.AddScoped<IGiamGiaService, GiamGiaService>();
builder.Services.AddScoped<INhanVienRepo, NhanVienRepo>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<ISaleRepo, SaleRepo>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISanPhamChiTietRepo, SanPhamChiTietRepo>();


builder.Services.AddScoped<IColorRepo, ColorRepo>();
builder.Services.AddScoped<IColorService, ColorService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
