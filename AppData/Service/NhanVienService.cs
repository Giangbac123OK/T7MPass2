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
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;

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
                Role = n.Role,
                Avatar = n.Avatar
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
                Role = nhanvien.Role,
                Avatar = nhanvien.Avatar
            };
        }
        public async Task SendAccountCreationEmail(string toEmail, string hoten, string password, int role)
        {
            await _repository.SendAccountCreationEmail(toEmail, hoten, password, role);
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
                Password = BCrypt.Net.BCrypt.HashPassword(nhanvienDto.Password),
                Role = nhanvienDto.Role, // 0: Admin, 1: Quản lý, 2: Nhân viên
                Ngaytaotaikhoan = nhanvienDto.Ngaytaotaikhoan,
                Avatar = nhanvienDto.Avatar
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
            nhanvien.Password = BCrypt.Net.BCrypt.HashPassword(nhanvienDto.Password);
            nhanvien.Avatar = nhanvienDto.Avatar;
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
                Role = n.Role,
                Avatar = n.Avatar
            });
        }
    }
}
