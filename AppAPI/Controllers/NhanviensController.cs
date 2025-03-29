﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Models;
using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Dto;
using Aspose.Email.Clients.Activity;
using Microsoft.Extensions.Hosting;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanviensController : ControllerBase
    {
        private readonly INhanVienService _Service;
        private readonly ILogger<LoginController> _logger;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public NhanviensController(INhanVienService service, ILogger<LoginController> logger, AppDbContext context, IWebHostEnvironment environment)
        {
            _Service = service;
            _logger = logger;
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Service.GetAllNhanviensAsync();
            return Ok(result.Select(nv => new
            {
                nv.Id,
                nv.Hovaten,
                nv.Ngaysinh,
                nv.Diachi,
                nv.Email,
                Gioitinh = nv.Gioitinh == true ? "Nam" : "Nữ",
                nv.Sdt,
                Trangthai = nv.Trangthai == 0 ? "Hoạt động" : "Dừng hoạt động",
                nv.Password,
                Role = nv.Role == 0 ? "Quản lý" : "Nhân viên"
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var nhanvien = await _Service.GetNhanvienByIdAsync(id);
                return Ok(new
                {
                    nhanvien.Id,
                    nhanvien.Hovaten,
                    nhanvien.Ngaysinh,
                    nhanvien.Diachi,
                    nhanvien.Email,
                    Gioitinh = nhanvien.Gioitinh == false ? "Nam" : "Nữ",
                    nhanvien.Sdt,
                    Trangthai = nhanvien.Trangthai == 0 ? "Hoạt động" : "Dừng hoạt động",
                    nhanvien.Password,
                    Role = nhanvien.Role == 0 ? "Quản lý" : "Nhân viên"
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Nhân viên không tồn tại.");
            }


        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NhanvienDTO nhanvienDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Xử lý avatar mặc định
            string avatarPath;
            const string defaultAvatarName = "AnhNhanVien.png";
            string physicalAvatarPath = Path.Combine(_environment.WebRootPath, "picture", defaultAvatarName);

            // Kiểm tra xem ảnh đã tồn tại trong thư mục chưa
            if (System.IO.File.Exists(physicalAvatarPath))
            {
                avatarPath = $"{defaultAvatarName}";
            }
            else
            {
                try
                {
                    // Thử tải ảnh từ URL dự phòng và lưu vào server
                    string imageUrl = "https://i.pinimg.com/736x/e9/3d/c7/e93dc787759b6a3451f99441684d77b3.jpg"; // URL dự phòng bạn cung cấp
                    using (HttpClient client = new HttpClient())
                    {
                        // Tải ảnh từ URL
                        byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

                        // Đảm bảo thư mục tồn tại
                        Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "picture"));

                        // Lưu ảnh vào server
                        await System.IO.File.WriteAllBytesAsync(physicalAvatarPath, imageBytes);

                        avatarPath = $"{defaultAvatarName}";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Không thể tải ảnh từ URL, sử dụng URL trực tiếp");
                    // Nếu không tải được, sử dụng URL trực tiếp
                    avatarPath = "https://i.pinimg.com/736x/e9/3d/c7/e93dc787759b6a3451f99441684d77b3.jpg";
                }
            }
            nhanvienDto.Avatar = avatarPath;
            await _Service.AddNhanvienAsync(nhanvienDto);
            return CreatedAtAction(nameof(GetById), new { id = nhanvienDto.Hovaten }, nhanvienDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NhanvienDTO nhanvienDto)
        {
            try
            {
                await _Service.UpdateNhanvienAsync(id, nhanvienDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Nhân viên không tồn tại.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _Service.DeleteNhanvienAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Nhân viên không tồn tại.");
            }
        }

        [HttpPost("Nhanvien/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO dto)
        {
            try
            {
                _logger.LogInformation($"Login attempt for email: {dto.Email}");
                if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                {
                    _logger.LogWarning("Email or password is empty");
                    return BadRequest("Email và mật khẩu không được để trống");
                }
                var khachHang = await _context.nhanviens
                    .FirstOrDefaultAsync(kh => kh.Email == dto.Email);
                if (khachHang == null)
                {
                    _logger.LogWarning($"Account not found for email: {dto.Email}");
                    return NotFound("Tài khoản không tồn tại");

                }
                bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, khachHang.Password);
                _logger.LogInformation($"Password validation result: {passwordValid}");
                if (!passwordValid)
                {
                    _logger.LogWarning($"Invalid password for email: {dto.Email}");
                    return Unauthorized("Mật khẩu không đúng");
                }
                _logger.LogInformation($"Login successful for email: {dto.Email}");
                return Ok(new
                {
                    Message = "Đăng nhập thành công",
                    id = khachHang.Id,
                    trangthai = khachHang.Trangthai,
                    Ten = khachHang.Hovaten,
                    email = khachHang.Email,
                    avatar = khachHang.Avatar,
                    role = khachHang.Role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login process");
                return StatusCode(500, "Đã xảy ra lỗi trong quá trình đăng nhập");
            }
        }

    }
}
