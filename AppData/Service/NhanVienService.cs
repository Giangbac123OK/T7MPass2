using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRepo _repository;
        public NhanVienService(INhanVienRepo repository)
        {
            _repository = repository;

        }
        public async Task<IEnumerable<NhanvienDTO>> GetAllNhanviensAsync()
        {
            var nhanviens = await _repository.GetAllAsync();
            return nhanviens.Select(n => new NhanvienDTO
            {
                Id = n.Id,
                Hovaten = n.Hovaten,
                Ngaysinh = n.Ngaysinh,
                Diachi = n.Diachi,
                Gioitinh = n.Gioitinh,
                Sdt = n.Sdt,
                Email = n.Email,
                Trangthai = n.Trangthai,
                Password = n.Password,
                Role = n.Role
            });
        }

        public async Task<NhanvienDTO> GetNhanvienByIdAsync(int id)
        {
            var nhanvien = await _repository.GetByIdAsync(id);
            if (nhanvien == null) throw new KeyNotFoundException("Nhân viên không tồn tại.");

            return new NhanvienDTO
            {
                Id = nhanvien.Id,
                Hovaten = nhanvien.Hovaten,
                Ngaysinh = nhanvien.Ngaysinh,
                Diachi = nhanvien.Diachi,
                Gioitinh = nhanvien.Gioitinh,
                Sdt = nhanvien.Sdt,
                Email = nhanvien.Email,
                Trangthai = nhanvien.Trangthai,
                Password = nhanvien.Password,
                Role = nhanvien.Role
            };
        }
        public async Task SendAccountCreationEmail(string toEmail, string hoten, string password, int role)
        {
            await _repository.SendAccountCreationEmail(toEmail, hoten, password, role);
        }
        private string GenerateSecurePassword(int length = 6)
        {
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specialChars = "!@#$%^&*";

            Random random = new Random();

            // Chọn ít nhất 1 ký tự từ mỗi nhóm
            char[] password = new char[length];
            password[0] = upperCase[random.Next(upperCase.Length)];
            password[1] = lowerCase[random.Next(lowerCase.Length)];
            password[2] = numbers[random.Next(numbers.Length)];
            password[3] = specialChars[random.Next(specialChars.Length)];

            // Các ký tự còn lại chọn ngẫu nhiên từ tất cả nhóm
            string allChars = upperCase + lowerCase + numbers + specialChars;
            for (int i = 4; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            // Trộn ngẫu nhiên thứ tự ký tự
            return new string(password.OrderBy(_ => random.Next()).ToArray());
        }
        public async Task AddNhanvienAsync(NhanvienDTO nhanvienDto)
        {
            var nhanvien = new Nhanvien
            {
                Hovaten = nhanvienDto.Hovaten,
                Ngaysinh = nhanvienDto.Ngaysinh,
                Diachi = nhanvienDto.Diachi,
                Email = nhanvienDto.Email,
                Gioitinh = nhanvienDto.Gioitinh,
                Sdt = nhanvienDto.Sdt,
                Trangthai = 0, // Mặc định "hoạt động",
                PasswordDefault = GenerateSecurePassword(),
                Password = GenerateSecurePassword(),
                Role = nhanvienDto.Role // 0: Admin, 1: Quản lý, 2: Nhân viên
            };
            await _repository.AddAsync(nhanvien);
        }
        public async Task ChangePasswordAfter24h()
        {
            var data = await _repository.GetAllAsync();
            foreach (var item in data)
            {
                if (DateTime.Now - item.Ngaytaotaikhoan >= TimeSpan.FromHours(24))
                {
                    item.Trangthai = 1;
                    await _repository.UpdateAsync(item);
                }
            }
        }
        public async Task UpdateNhanvienAsync(int id, NhanvienDTO nhanvienDto)
        {
            var nhanvien = await _repository.GetByIdAsync(id);
            if (nhanvien == null) throw new KeyNotFoundException("Nhân viên không tồn tại.");

            nhanvien.Hovaten = nhanvienDto.Hovaten;
            nhanvien.Ngaysinh = nhanvienDto.Ngaysinh;
            nhanvien.Diachi = nhanvienDto.Diachi;
            nhanvien.Gioitinh = nhanvienDto.Gioitinh;
            nhanvien.Sdt = nhanvienDto.Sdt;
            nhanvien.Password = nhanvienDto.Password;

            await _repository.UpdateAsync(nhanvien);
        }

        public async Task DeleteNhanvienAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task<IEnumerable<NhanvienDTO>> TimKiemNhanvienAsync(string search)
        {
            var nhanviens = await _repository.TimKiemNhanvienAsync(search);
            return nhanviens.Select(n => new NhanvienDTO
            {
                Id = n.Id,
                Hovaten = n.Hovaten,
                Ngaysinh = n.Ngaysinh,
                Diachi = n.Diachi,
                Gioitinh = n.Gioitinh,
                Sdt = n.Sdt,
                Trangthai = n.Trangthai,
                Password = n.Password,
                Role = n.Role
            });
        }
    }
}
