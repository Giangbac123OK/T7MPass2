using AppData;
using AppData.IRepository;
using AppData.IService;
using AppData.IService_Admin;
using AppData.Repository;
using AppData.Service;
using AppData.Service_Admin;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using SaleService = AppData.Service.SaleService;

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


IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

PayOS payOS = new PayOS(configuration["Environment:PAYOS_CLIENT_ID"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_API_KEY"] ?? throw new Exception("Cannot find environment"),
                    configuration["Environment:PAYOS_CHECKSUM_KEY"] ?? throw new Exception("Cannot find environment"));

// Thêm các dịch vụ vào DI container
builder.Services.AddControllers();
builder.Services.AddScoped<IPhuongThucThanhToanRepo, PhuongThucThanhToanRepo>();
builder.Services.AddScoped<IPhuongThucThanhToanService, PhuongThucThanhToanService>();
builder.Services.AddScoped<ISaleRepo, SaleRepo>();
builder.Services.AddScoped<AppData.IService.ISaleService, SaleService>();
builder.Services.AddScoped<ISanPhamChiTietRepo, SanPhamChiTietRepo>();
builder.Services.AddScoped<ISanPhamChiTietService, SanPhamChiTietService>();
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
builder.Services.AddScoped<IThongKeSanPhamRepo, ThongKeSanPhamRepo>();
builder.Services.AddScoped<IThongkeService, ThongkeService>();
builder.Services.AddScoped<IsaleRepos, SaleRepos>();
builder.Services.AddScoped<IsalechitietRepos , SaleechitietRepos>();
builder.Services.AddScoped<IsanphamRepos, SanphamRepos>();
builder.Services.AddScoped<ISanphamchitietRepository, SanphamchitietRepository>();
builder.Services.AddScoped<AppData.IService_Admin.ISaleService, AppData.Service_Admin.SaleService>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(payOS);
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

app.UseStaticFiles(); // Cho phép truy cập ảnh trong wwwroot

app.UseAuthorization();

app.MapControllers();

app.UseRouting();
app.Run();

