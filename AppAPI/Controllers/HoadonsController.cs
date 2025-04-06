using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Models;
using AppData.DTO;
using AppData.IService;
using AppData.IRepository;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoadonsController : ControllerBase
    {
        private readonly IHoaDonService _Service;
        private readonly IHoaDonChiTietService _HDCTservice;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ISizeRepo _Sizerepository;
        private readonly IChatLieuRepo _Chatlieurepository;
        private readonly IColorRepo _Colorrepository;
        private readonly IHoaDonChiTietService _HDCTrepository;
        private readonly ISanPhamChiTietRepo _SPCTrepository;
        private readonly ISanPhamRepo _SPrepository;
        private readonly IHoaDonRepo _repository;
        private readonly INhanVienRepo _NVrepository;
        private readonly IKhachHangRepo _KHrepository;
        private readonly IWebHostEnvironment _env;
        private readonly string _filePath;

        public HoadonsController(IHoaDonService service, AppDbContext context, IHoaDonChiTietService HDCTservice, IHoaDonRepo repository, INhanVienRepo NVrepository, IKhachHangRepo KHrepository, IHoaDonChiTietService hoaDonChiTietService, IConfiguration configuration, ISanPhamChiTietRepo sPCTrepository, ISanPhamRepo sPrepository, ISizeRepo sizeRepo, IColorRepo colorRepo, IChatLieuRepo chatLieuRepo, IWebHostEnvironment env)
        {
            _Service = service;
            _context = context;
            _HDCTservice = HDCTservice;
            _configuration = configuration;
            _HDCTrepository = hoaDonChiTietService;
            _SPCTrepository = sPCTrepository;
            _SPrepository = sPrepository;
            _Sizerepository = sizeRepo;
            _Chatlieurepository = chatLieuRepo;
            _Colorrepository = colorRepo;
            _repository = repository;
            _NVrepository = NVrepository;
            _KHrepository = KHrepository;
            _env = env;
            _filePath = Path.Combine(_env.WebRootPath, "data", "read_orders.json");

            // Đảm bảo thư mục tồn tại
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        [HttpPut("da-nhan-don-hang-{id}")]
        public async Task<IActionResult> Danhandonhang(int id)
        {
            try
            {
                await _Service.Danhandonhang(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // API để lấy tất cả hoá đơn
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hoadonList = await _Service.GetAllAsync();
            return Ok(hoadonList);
        }

        // API để lấy hoá đơn theo Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoadon = await _Service.GetByIdAsync(id);
            if (hoadon == null) return NotFound(new { message = "Hoá đơn không tìm thấy" });
            return Ok(hoadon);
        }

        // API để thêm hoá đơn
        [HttpPost]
        public async Task<IActionResult> Add(HoadonDTO dto)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            try
            {
                if (dto.Idgg > 0)
                {
                    var giamgia = await _context.giamgias.FirstOrDefaultAsync(id => (int?)id.Id == dto.Idgg);
                    if (giamgia == null)
                    {
                        return NotFound(new { message = "Id giảm giá không tìm thấy" });
                    }
                    // Cập nhật số lượng
                    giamgia.Soluong -= 1;
                    _context.giamgias.Update(giamgia);

                    // Lưu thay đổi vào DB
                    await _context.SaveChangesAsync();
                }

                // Thêm hóa đơn
                await _Service.AddAsync(dto);

                // Trả về ID của hóa đơn mới được tạo
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi thêm hoá đơn
                return StatusCode(500, new { message = "Lỗi khi thêm hoá đơn", error = ex.Message });
            }
        }

        [HttpPost("SendOrderSuccessEmail/{id}")]
        public async Task<IActionResult> SendOrderSuccessEmail(int id)
        {
            try
            {
                // Lấy thông tin hóa đơn
                var hoadon = await _repository.GetByIdAsync(id);
                if (hoadon == null) return NotFound(new { message = "Hóa đơn không tồn tại" });

                // Lấy thông tin khách hàng
                var khachhang = await _KHrepository.GetByIdAsync(hoadon.Idkh ?? 0);
                if (khachhang == null) return NotFound(new { message = "Khách hàng không tồn tại" });

                // Lấy danh sách hóa đơn chi tiết
                var hoadonchitiet = await _HDCTrepository.HoadonchitietTheoMaHD(hoadon.Id);
                if (!hoadonchitiet.Any()) return NotFound(new { message = "Hóa đơn không có chi tiết" });

                // Lấy danh sách sản phẩm chi tiết
                var sanphamchitietList = new List<Sanphamchitiet>();
                foreach (var ct in hoadonchitiet)
                {
                    var spct = await _SPCTrepository.GetByIdAsync(ct.Idspct);
                    if (spct != null) sanphamchitietList.Add(spct);
                }

                // Lấy danh sách sản phẩm từ SPCT
                var sanphamDict = new Dictionary<int, Sanpham>();
                foreach (var spct in sanphamchitietList)
                {
                    if (!sanphamDict.ContainsKey(spct.Idsp))
                    {
                        var sp = await _SPrepository.GetByIdAsync(spct.Idsp);
                        if (sp != null) sanphamDict[spct.Idsp] = sp;
                    }
                }

                // Kiểm tra cấu hình email
                var emailSettings = _configuration.GetSection("EmailSettings");
                var senderEmail = emailSettings["SenderEmail"] ?? throw new InvalidOperationException("Sender email not configured");
                var senderPassword = emailSettings["SenderPassword"] ?? throw new InvalidOperationException("Sender password not configured");
                var smtpServer = emailSettings["SmtpServer"] ?? throw new InvalidOperationException("SMTP server not configured");
                var smtpPort = int.Parse(emailSettings["SmtpPort"] ?? throw new InvalidOperationException("SMTP port not configured"));

                // Lấy base URL của API
                string baseUrl = $"{Request.Scheme}://{Request.Host}";

                // Tạo danh sách sản phẩm trong email
                var linkedResources = new List<LinkedResource>();
                var productListHtml = new StringBuilder();

                foreach (var ct in hoadonchitiet)
                {
                    var spct = sanphamchitietList.FirstOrDefault(x => x.Id == ct.Idspct);
                    if (spct == null || !sanphamDict.TryGetValue(spct.Idsp, out var sp)) continue;

                    var mau = await _Colorrepository.GetByIdAsync(spct.IdMau);
                    var size = await _Sizerepository.GetByIdAsync(spct.IdSize);
                    var chatlieu = await _Chatlieurepository.GetByIdAsync(spct.IdChatLieu);

                    string imageUrl = $"{baseUrl}/api/Sanphamchitiets/GetImageById/{spct.Id}";
                    var imageBytes = await new HttpClient().GetByteArrayAsync(imageUrl);
                    var linkedResource = new LinkedResource(new MemoryStream(imageBytes), "image/jpeg")
                    {
                        ContentId = $"image{spct.Id}"
                    };
                    linkedResources.Add(linkedResource);

                    string priceHtml = ct.Giamgia == 0
                        ? $"<td>{ct.Giasp:#,##0 VNĐ}</td>"
                        : $"<td><del>{ct.Giasp:#,##0 VNĐ}</del> {ct.Giamgia:#,##0 VNĐ}</td>";

                    productListHtml.Append($@"
            <tr>
                <td><img src='cid:image{spct.Id}' width='50' height='50'></td>
                <td>{sp.TenSanpham}</td>
                <td>{mau?.Tenmau ?? "N/A"}</td>
                <td>{size?.Sosize ?? 0}</td>
                <td>{chatlieu?.Tenchatlieu ?? "N/A"}</td>
                <td>{ct.Soluong}</td>
                {priceHtml}
                <td>{ct.Soluong * (ct.Giasp - (ct.Giamgia ?? 0)):#,##0 VNĐ}</td>
            </tr>");
                }

                // Nội dung email
                var body = $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
        .container {{ width: 100%; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px; box-shadow: 2px 2px 10px rgba(0,0,0,0.1); }}
        .header {{ text-align: center; font-size: 20px; font-weight: bold; color: #4CAF50; }}
        .content {{ margin-top: 20px; font-size: 16px; }}
        .footer {{ margin-top: 30px; font-size: 14px; color: #777; text-align: center; }}
        table {{ width: 100%; border-collapse: collapse; margin-top: 10px; }}
        th, td {{ padding: 10px; border: 1px solid #ddd; text-align: left; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>Cảm ơn bạn đã mua hàng tại cửa hàng chúng tôi!</div>
        <div class='content'>
            <p>Xin chào <b>{khachhang.Ten}</b>,</p>
            <p>Chúng tôi rất vui thông báo rằng đơn hàng của bạn đã được đặt thành công.</p>
            <p><b>Mã đơn hàng:</b> {hoadon.Id}</p>
            <p><b>Ngày đặt hàng:</b> {hoadon.Thoigiandathang:dd/MM/yyyy}</p>
            <p><b>Tổng tiền sản phẩm:</b> {hoadon.Tongtiensanpham:#,##0 VNĐ}</p>
            <p><b>Số tiền giảm:</b> {hoadon.Tonggiamgia:#,##0 VNĐ}</p>
            <p><b>Phí vận chuyển:</b> {hoadon.Phivanchuyen:#,##0 VNĐ}</p>
            <p><b>Tổng tiền hóa đơn:</b> {hoadon.Tongtiencantra:#,##0 VNĐ}</p>
            <p><b>Chi tiết đơn hàng:</b></p>
            <table>
                <tr>
                    <th>Ảnh</th>
                    <th>Tên sản phẩm</th>
                    <th>Màu</th>
                    <th>Size</th>
                    <th>Chất liệu</th>
                    <th>Số lượng</th>
                    <th>Giá</th>
                    <th>Thành tiền</th>
                </tr>
                {productListHtml}
            </table>
            <p>Chúng tôi sẽ sớm liên hệ để xác nhận đơn hàng của bạn.</p>
            <p>Trân trọng,</p>
            <p><b>Đội ngũ hỗ trợ</b></p>
        </div>
        <div class='footer'>Mọi thắc mắc, vui lòng liên hệ hotline: 1800-xxxx</div>
    </div>
</body>
</html>";

                // Gửi email
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Xác nhận đặt hàng thành công",
                    IsBodyHtml = true
                };
                var htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                linkedResources.ForEach(lr => htmlView.LinkedResources.Add(lr));
                mailMessage.AlternateViews.Add(htmlView);
                mailMessage.To.Add(khachhang.Email);

                using var smtpClient = new SmtpClient(smtpServer) { Port = smtpPort, Credentials = new NetworkCredential(senderEmail, senderPassword), EnableSsl = true };
                await smtpClient.SendMailAsync(mailMessage);

                return Ok(new { message = "Email xác nhận đã được gửi thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi khi gửi email: {ex.Message}" });
            }
        }




        [HttpGet("voucher/{id}")]
        public async Task<IActionResult> Checkvoucher(int id)
        {
            // Lấy dữ liệu hóa đơn từ service
            var hoadon = await _Service.Checkvoucher(id);

            // Nếu không tìm thấy hóa đơn, trả về null
            if (hoadon == null)
                return Ok(null); // Trả về null nếu không có dữ liệu

            // Nếu có dữ liệu, trả về hóa đơn
            return Ok(hoadon);
        }


        // API để cập nhật hoá đơn
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HoadonDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            var existingHoadon = await _Service.GetByIdAsync(id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            try
            {
                await _Service.UpdateAsync(dto, id);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi cập nhật hoá đơn
                return StatusCode(500, new { message = "Lỗi khi cập nhật hoá đơn", error = ex.Message });
            }
        }

        // API để cập nhật hoá đơn
        [HttpPut("trangthai/{id}")]
        public async Task<IActionResult> Updatetrangthai(int id, int trangthai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            var existingHoadon = await _context.hoadons.FirstOrDefaultAsync(kh => kh.Id == id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            if (existingHoadon.Idgg != null && trangthai == 4)
            {
                var voucher = await _context.giamgias.FirstOrDefaultAsync(kh => kh.Id == existingHoadon.Idgg);
                if (voucher == null)
                {
                    return NotFound(new { message = "Voucher không tìm thấy" });
                }
                // Giảm số lượng mã giảm giá
                voucher.Soluong += 1;
                _context.giamgias.Update(voucher);
                await _context.SaveChangesAsync();
            }

            if (existingHoadon.Trangthai == 4)
            {
                await _HDCTservice.ReturnProductAsync(id);
            }

            // Cập nhật điểm khách hàng nếu trạng thái là 3 và có sử dụng điểm
            if (trangthai == 3)
            {
                var khachhang = await _context.khachhangs.FirstOrDefaultAsync(kh => kh.Id == existingHoadon.Idkh);
                if (khachhang == null)
                {
                    return NotFound(new { message = "Khách hàng không tìm thấy" });
                }

                // Tính điểm từ hoá đơn và cập nhật điểm khách hàng
                int diemhoadon = (int)existingHoadon.Tongtiencantra / 100; // Quy đổi 100 VND = 1 điểm
                khachhang.Diemsudung += diemhoadon;
                khachhang.Tichdiem += diemhoadon;

                // Kiểm tra và cập nhật rank khách hàng
                var currentRank = await _context.ranks.FirstOrDefaultAsync(r => r.Id == khachhang.Idrank);
                if (currentRank != null && khachhang.Tichdiem > currentRank.MaxMoney)
                {
                    // Tìm rank tiếp theo phù hợp với điểm hiện tại
                    var nextRank = await _context.ranks
                        .Where(r => r.MinMoney <= khachhang.Tichdiem)
                        .OrderByDescending(r => r.MinMoney)
                        .FirstOrDefaultAsync();

                    if (nextRank != null && nextRank.Id != currentRank.Id)
                    {
                        khachhang.Idrank = nextRank.Id; // Cập nhật rank mới
                    }
                }

                _context.khachhangs.Update(khachhang);
                await _context.SaveChangesAsync();
            }

            try
            {
                existingHoadon.Trangthai = trangthai;

                _context.hoadons.Update(existingHoadon);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi cập nhật hoá đơn",
                    error = ex.Message
                });
            }
        }

        // API để cập nhật hoá đơn
        [HttpPut("trangthaiNV/{id}")]
        public async Task<IActionResult> UpdatetrangthaiNV(int id, int trangthai, int idnv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            var existingHoadon = await _context.hoadons.FirstOrDefaultAsync(kh => kh.Id == id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            var existingNV = await _context.nhanviens.FirstOrDefaultAsync(kh => kh.Id == idnv);
            if (existingNV == null)
            {
                return NotFound(new { message = "Nhân viên không tìm thấy" });
            }

            if (existingHoadon.Idgg != null && trangthai == 4)
            {
                var voucher = await _context.giamgias.FirstOrDefaultAsync(kh => kh.Id == existingHoadon.Idgg);
                if (voucher == null)
                {
                    return NotFound(new { message = "Voucher không tìm thấy" });
                }
                // Giảm số lượng mã giảm giá
                voucher.Soluong += 1;
                _context.giamgias.Update(voucher);
                await _context.SaveChangesAsync();
            }

            if (existingHoadon.Trangthai == 4)
            {
                await _HDCTservice.ReturnProductAsync(id);
            }

            // Cập nhật điểm khách hàng nếu trạng thái là 3 và có sử dụng điểm
            if (trangthai == 3)
            {
                var khachhang = await _context.khachhangs.FirstOrDefaultAsync(kh => kh.Id == existingHoadon.Idkh);
                if (khachhang == null)
                {
                    return NotFound(new { message = "Khách hàng không tìm thấy" });
                }

                // Tính điểm từ hoá đơn và cập nhật điểm khách hàng
                int diemhoadon = (int)existingHoadon.Tongtiencantra / 100; // Quy đổi 100 VND = 1 điểm
                khachhang.Diemsudung += diemhoadon;
                khachhang.Tichdiem += diemhoadon;

                // Kiểm tra và cập nhật rank khách hàng
                var currentRank = await _context.ranks.FirstOrDefaultAsync(r => r.Id == khachhang.Idrank);
                if (currentRank != null && khachhang.Tichdiem > currentRank.MaxMoney)
                {
                    // Tìm rank tiếp theo phù hợp với điểm hiện tại
                    var nextRank = await _context.ranks
                        .Where(r => r.MinMoney <= khachhang.Tichdiem)
                        .OrderByDescending(r => r.MinMoney)
                        .FirstOrDefaultAsync();

                    if (nextRank != null && nextRank.Id != currentRank.Id)
                    {
                        khachhang.Idrank = nextRank.Id; // Cập nhật rank mới
                    }
                }

                _context.khachhangs.Update(khachhang);
                await _context.SaveChangesAsync();
            }

            try
            {
                existingHoadon.Trangthai = trangthai;
                existingHoadon.Idnv = idnv;

                _context.hoadons.Update(existingHoadon);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi khi cập nhật hoá đơn",
                    error = ex.Message
                });
            }
        }

        // API để cập nhật hoá đơn
        [HttpPut("trangthaitrahang/{id}")]
        public async Task<IActionResult> Updatetrangthaitrahang(int id, int trangthai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            var existingHoadon = await _context.hoadons.FirstOrDefaultAsync(kh => kh.Id == id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            try
            {
                existingHoadon.Trangthai = trangthai;

                _context.hoadons.Update(existingHoadon);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = existingHoadon.Id }, existingHoadon);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi cập nhật hoá đơn
                return StatusCode(500, new { message = "Lỗi khi cập nhật hoá đơn", error = ex.Message });
            }
        }

        // API để cập nhật hoá đơn
        [HttpPut("CheckTraHang/{id}")]
        public async Task<IActionResult> CheckTraHang(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            // B1: Lấy danh sách hóa đơn chi tiết theo `idhd`
            var hoadonchitietList = await _context.hoadonchitiets
                .Where(hdct => hdct.Idhd == id)
                .ToListAsync();

            if (hoadonchitietList == null || !hoadonchitietList.Any())
            {
                return NotFound(new { message = "Không tìm thấy danh sách hóa đơn chi tiết cho hóa đơn này" });
            }

            // B2: Kiểm tra từng hóa đơn chi tiết có tồn tại trong bảng trả hàng chi tiết
            var idsChuaTonTai = new List<int>();
            foreach (var hdct in hoadonchitietList)
            {
                var existsInTraHangChiTiet = await _context.trahangchitiets
                    .AnyAsync(thct => thct.Idhdct == hdct.Id);

                if (!existsInTraHangChiTiet)
                {
                    idsChuaTonTai.Add(hdct.Id); // Thêm idhdct chưa tồn tại vào danh sách
                }
            }

            // B3.1: Nếu tất cả hóa đơn chi tiết đều tồn tại trong bảng trả hàng chi tiết
            if (!idsChuaTonTai.Any())
            {
                return Ok(new { result = true, message = "Tất cả hóa đơn chi tiết đã tồn tại trong bảng trả hàng chi tiết" });
            }

            // B3.2: Nếu vẫn còn một số hóa đơn chi tiết chưa tồn tại
            return Ok(new
            {
                result = false,
                message = "Một số hóa đơn chi tiết chưa tồn tại trong bảng trả hàng chi tiết",
                missingIds = idsChuaTonTai
            });
        }


        // API để xóa hoá đơn
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHoadon = await _Service.GetByIdAsync(id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            try
            {
                await _Service.DeleteAsync(id);
                return NoContent(); // Trả về status code 204 nếu xóa thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi xóa hoá đơn
                return StatusCode(500, new { message = "Lỗi khi xóa hoá đơn", error = ex.Message });
            }
        }
        [HttpGet("hoa-don-theo-ma-kh-{id}")]
        public async Task<IActionResult> Hoadontheomakh(int id, [FromQuery] string? search)
        {
            try
            {
                return Ok(await _Service.TimhoadontheoIdKH(id, search));
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi xóa hoá đơn
                return StatusCode(500, new { message = "Lỗi khi xóa hoá đơn", error = ex.Message });
            }
        }

        [HttpGet("unread-orders")]
        public IActionResult GetUnreadOrders()
        {
            var allOrders = GetBaseOrdersQuery();
            var readOrderIds = GetReadOrderIds();

            var unreadOrders = allOrders
                .Where(o => !readOrderIds.Contains(o.Id))
                .ToList();

            return Ok(unreadOrders);
        }

        [HttpGet("read-orders")]
        public IActionResult GetReadOrders()
        {
            var allOrders = GetBaseOrdersQuery();
            var readOrderIds = GetReadOrderIds();

            var readOrders = allOrders
                .Where(o => readOrderIds.Contains(o.Id))
                .ToList();

            return Ok(readOrders);
        }

        [HttpGet("old-orders")]
        public IActionResult GetOldOrders()
        {
            var cutoffDate = DateTime.Now.AddDays(-7);
            var allOrders = GetBaseOrdersQuery()
                .Where(o => o.OrderDate < cutoffDate)
                .ToList();

            return Ok(allOrders);
        }

        private IQueryable<OrderNotificationDto> GetBaseOrdersQuery()
        {
            return _context.hoadons
                .OrderByDescending(o => o.Thoigiandathang)
                .Select(o => new OrderNotificationDto
                {
                    Id = o.Id,
                    OrderCode = $"HD{o.Id.ToString().PadLeft(6, '0')}",
                    OrderDate = o.Thoigiandathang,
                    TotalAmount = o.Tongtiencantra,
                    CustomerName = o.Khachhang != null ? o.Khachhang.Ten : "Khách vãng lai",
                    Status = o.TrangthaiStr
                });
        }

        private List<int> GetReadOrderIds()
        {
            if (System.IO.File.Exists(_filePath))
            {
                var json = System.IO.File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<int>>(json) ?? new List<int>();
            }
            return new List<int>();
        }
    }
}
