using AppData;
using AppData.IRepository;
using AppData.IService;
using AppData.Repository;
using AppData.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Thêm cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()   // Cho phép tất cả nguồn (Frontend có thể gọi API)
               .AllowAnyMethod()   // Cho phép tất cả HTTP methods (GET, POST, PUT, DELETE, ...)
               .AllowAnyHeader();  // Cho phép tất cả headers
    });
});

// Thêm DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm các dịch vụ vào DI container
builder.Services.AddControllers();
builder.Services.AddScoped<IPhuongThucThanhToanRepo, PhuongThucThanhToanRepo>();
builder.Services.AddScoped<IPhuongThucThanhToanService, PhuongThucThanhToanService>();
builder.Services.AddScoped<ISaleRepo, SaleRepo>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISanPhamChiTietRepo, SanPhamChiTietRepo>();
builder.Services.AddScoped<IColorRepo, ColorRepo>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IChatLieuRepo, ChatLieuRepo>();
builder.Services.AddScoped<IChatLieuService, ChatLieuService>();
builder.Services.AddScoped<IDanhGiaRepo, DanhGiaRepo>();
builder.Services.AddScoped<IDanhGiaService, DanhGiaService>();
builder.Services.AddScoped<IDiaChiService, DiaChiService>();
builder.Services.AddScoped<IDiaChiRepo, DiaChiRepo>();
builder.Services.AddScoped<IGiamGia_RankRepo, GiamGia_RankRepo>();
builder.Services.AddScoped<IGiamGia_RankService, GiamGia_RankService>();
builder.Services.AddScoped<IGiamGiaRepo, GiamGiaRepo>();
builder.Services.AddScoped<IGiamGiaService, GiamGiaService>();
builder.Services.AddScoped<IGioHangRepo, GioHangRepo>();
builder.Services.AddScoped<IGioHangService, GioHangService>();
builder.Services.AddScoped<IGioHangChiTetRepo, GioHangChiTetRepo>();
builder.Services.AddScoped<IGioHangChiTetService, GioHangChiTetService>();
builder.Services.AddScoped<IHinhAnhRepo, HinhAnhRepo>();
builder.Services.AddScoped<IHinhAnhService, HinhAnhService>();
builder.Services.AddScoped<IHoaDonRepo, HoaDonRepo>();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();
builder.Services.AddScoped<IHoaDonChiTietRepo, HoaDonChiTetRepo>();
builder.Services.AddScoped<IHoaDonChiTietService, HoaDonChiTietService>();
builder.Services.AddScoped<IKhachHangRepo, KhachHangRepo>();
builder.Services.AddScoped<IKhachHangService, KhachHangService>();
builder.Services.AddScoped<INhanVienRepo, NhanVienRepo>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<IRankRepo, RankRepo>();
builder.Services.AddScoped<IRankService, RankService>();
builder.Services.AddScoped<ISaleChiTietRepo, SaleChiTietRepo>();
builder.Services.AddScoped<ISaleChiTietService, SaleChiTietService>();
builder.Services.AddScoped<ISanPhamRepo, SanPhamRepo>();
builder.Services.AddScoped<ISanPhamService, SanPhamService>();
builder.Services.AddScoped<ISizeRepo, SizeRepo>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IThuongHieuRepo, ThuongHieuRepo>();
builder.Services.AddScoped<IThuongHieuService, ThuongHieuService>();
builder.Services.AddScoped<ITraHangRepo, TraHangRepo>();
builder.Services.AddScoped<ITraHangService, TraHangService>();
builder.Services.AddScoped<ITraHangChiTietRepo, TraHangChiTietRepo>();
builder.Services.AddScoped<ITraHangChiTietService, TraHangChiTietService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// 🔹 Cấu hình Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔹 Đặt CORS ngay sau HTTPS Redirection
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
