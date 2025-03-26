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
using AppData.Dto;
using Microsoft.Extensions.Hosting;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachhangsController : ControllerBase
    {
        private readonly IKhachHangService _Service;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public KhachhangsController(IKhachHangService ser, AppDbContext context, IWebHostEnvironment environment)
        {
            _Service = ser;
            _context = context;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _Service.GetAllKhachhangsAsync();
            return Ok(result.Select(kh => new
            {
                kh.Id,
                kh.Ten,
                kh.Sdt,
                kh.Ngaysinh,
                kh.Tichdiem,
                kh.Email,
                kh.Password,
                kh.Diemsudung,
                Trangthai = kh.Trangthai == 0 ? "Hoạt động" : "Tài khoản bị khoá",
                kh.Idrank,
                kh.Gioitinh,
                kh.Avatar
            }));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var kh = await _Service.GetKhachhangByIdAsync(id);
                return Ok(new
                {

                    kh.Ten,
                    kh.Sdt,
                    kh.Ngaysinh,
                    kh.Tichdiem,
                    kh.Email,
                    kh.Password,
                    kh.Diemsudung,
                    Trangthai = kh.Trangthai == 0 ? "Hoạt động" : "Tài khoản bị khoá",
                    kh.Idrank,
                    kh.Gioitinh,
                    kh.Avatar
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Khách hàng không tồn tại.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(KhachhangDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _Service.AddKhachhangAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Ten }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] KhachhangDTO dto)
        {
            try
            {
                await _Service.UpdateKhachhangAsync(id, dto);

                if (dto.Trangthai == 1)
                {
                    return Ok(new { message = "Tài khoản đã bị khóa." });
                }
                return Ok(new { message = "Cập nhật thông tin thành công." });
            }

            catch (KeyNotFoundException)
            {
                return NotFound("Khách hàng không tồn tại.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPut("diem/{id}")]
        public async Task<IActionResult> UpdateDiem(int id, int diemsudung)
        {
            try
            {
                var checkhachang = await _context.khachhangs.FirstOrDefaultAsync(kh => kh.Id == id);
                if (checkhachang == null)
                {
                    return NotFound("Khách hàng không tồn tại.");
                }
                checkhachang.Diemsudung = diemsudung;

                _context.khachhangs.Update(checkhachang);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Cập nhật thông tin thành công." });
            }

            catch (KeyNotFoundException)
            {
                return NotFound("Khách hàng không tồn tại.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        [HttpPut("UpdateThongTinKhachhangAsync/{id}")]
        public async Task<IActionResult> UpdateThongTinKhachhangAsync(int id, [FromBody] KhachhangDTO dto)
        {
            try
            {
                await _Service.UpdateThongTinKhachhangAsync(id, dto);

                if (dto.Trangthai == 1)
                {
                    return Ok(new { message = "Tài khoản đã bị khóa." });
                }
                return Ok(new { message = "Cập nhật thông tin thành công." });
            }

            catch (KeyNotFoundException)
            {
                return NotFound("Khách hàng không tồn tại.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _Service.DeleteKhachhangAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Khách hàng không tồn tại.");
            }
        }
        [HttpGet("find-khachhang")]
        public async Task<IActionResult> FindByEmailAsync(string email)
        {
            var nhanVien = await _Service.FindByEmailAsync(email);
            if (nhanVien == null)
                return NotFound("Khách hàng không tồn tại.");

            return Ok(nhanVien);
        }
        // API gửi mã OTP cho quên mật khẩu
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtpAsync(ForgotPasswordRequestKHDto dto)
        {
            var (isSent, otp) = await _Service.SendOtpAsync(dto.Email); // Nhận kết quả và OTP từ service

            if (!isSent)
                return BadRequest("Gửi OTP không thành công.");

            return Ok(new { success = true, message = "Mã OTP đã được gửi.", otp }); // Trả OTP cho client (chỉ khi cần, thường dùng trong môi trường phát triển)
        }
        [HttpPost("doimatkhau")]
        public async Task<IActionResult> ChangePassword([FromBody] DoimkKhachhang changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var khachHang = await _context.khachhangs.FirstOrDefaultAsync(kh => kh.Email == changePasswordDto.Email);
                if (khachHang == null)
                {
                    return NotFound(new { message = "Tài khoản không tồn tại" });
                }

                // Kiểm tra mật khẩu cũ
                bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, khachHang.Password);
                if (!isOldPasswordValid)
                {
                    return Unauthorized(new { message = "Mật khẩu cũ không chính xác" });
                }

                // Hash mật khẩu mới
                khachHang.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);

                _context.khachhangs.Update(khachHang);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Đổi mật khẩu thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi trong quá trình đổi mật khẩu", error = ex.Message });
            }
        }
        [HttpPost("quenmatkhau")]
        public async Task<IActionResult> quenmatkhau([FromBody] DoimkKhachhang changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var khachHang = await _context.khachhangs.FirstOrDefaultAsync(kh => kh.Email == changePasswordDto.Email);
                if (khachHang == null)
                {
                    return NotFound(new { message = "Tài khoản không tồn tại" });
                }

                // Kiểm tra mật khẩu cũ
                var isOldPasswordValid = khachHang.Password;

                // Hash mật khẩu mới
                khachHang.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);

                _context.khachhangs.Update(khachHang);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Đổi mật khẩu thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi trong quá trình đổi mật khẩu", error = ex.Message });
            }
        }

        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] IFormFile image, [FromForm] string oldFileName = null)
        {
            try
            {
                // Kiểm tra xem có file được gửi lên không
                if (image == null || image.Length == 0)
                {
                    return BadRequest("Không có file được tải lên");
                }

                // Kiểm tra định dạng file
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Chỉ chấp nhận file ảnh có định dạng .png, .jpg hoặc .jpeg");
                }

                // Kiểm tra kích thước file (tối đa 10MB)
                if (image.Length > 10 * 1024 * 1024)
                {
                    return BadRequest("Kích thước file quá lớn (tối đa 10MB)");
                }

                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(oldFileName))
                {
                    var oldFilePath = Path.Combine(_environment.WebRootPath, "picture", oldFileName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Tạo tên file mới (sử dụng Guid để tránh trùng lặp)
                var newFileName = $"{Guid.NewGuid()}{fileExtension}";
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "picture");

                // Đảm bảo thư mục tồn tại
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, newFileName);

                // Lưu file vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                // Trả về đường dẫn tương đối của ảnh
                var imageUrl = $"/picture/{newFileName}";
                return Ok(new
                {
                    success = true,
                    imageUrl = imageUrl,
                    fileName = newFileName,
                    oldFileNameDeleted = oldFileName
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xử lý file: {ex.Message}");
            }
        }
    }
}
