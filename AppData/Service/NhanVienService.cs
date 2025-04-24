﻿using AppData.DTO;
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
            return nhanviens.Where(x=>x.Trangthai==1||x.Trangthai==0).Select(n => new NhanvienDTO
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
                Avatar = n.Avatar,
                Ngaytaotaikhoan = n.Ngaytaotaikhoan
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
                Avatar = nhanvien.Avatar,
                Ngaytaotaikhoan = nhanvien.Ngaytaotaikhoan
            };
        }
        public async Task<int> AddNhanvienAsync(NhanvienDTO nhanvienDto)
        {
            var list = await _repository.GetAllAsync();

            var checkEmail = list.FirstOrDefault(x => x.Email == nhanvienDto.Email&&x.Trangthai==2);
            var checkSdt = list.FirstOrDefault(x => x.Sdt == nhanvienDto.Sdt && x.Trangthai == 2);

            // Nếu cả Email và SĐT đều chưa tồn tại → thêm mới
            if (checkEmail == null && checkSdt == null)
            {
                var nhanvien = new Nhanvien
                {
                    Hovaten = nhanvienDto.Hovaten,
                    Ngaysinh = nhanvienDto.Ngaysinh,
                    Diachi = nhanvienDto.Diachi,
                    Email = nhanvienDto.Email,
                    Gioitinh = nhanvienDto.Gioitinh,
                    Sdt = nhanvienDto.Sdt,
                    Trangthai = 0,
                    Password = BCrypt.Net.BCrypt.HashPassword(nhanvienDto.Password),
                    Role = nhanvienDto.Role,
                    Ngaytaotaikhoan = nhanvienDto.Ngaytaotaikhoan,
                    Avatar = nhanvienDto.Avatar
                };

                var result = _repository.AddAsync(nhanvien);
                return result.Id;
            }

            // Nếu một trong hai đã tồn tại, ưu tiên cập nhật theo Email nếu có
            var existing = checkEmail ?? checkSdt;

            existing.Hovaten = nhanvienDto.Hovaten;
            existing.Ngaysinh = nhanvienDto.Ngaysinh;
            existing.Diachi = nhanvienDto.Diachi;
            existing.Gioitinh = nhanvienDto.Gioitinh;
            existing.Trangthai = 0;
            existing.Password = BCrypt.Net.BCrypt.HashPassword(nhanvienDto.Password);
            existing.Role = nhanvienDto.Role;
            existing.Ngaytaotaikhoan = nhanvienDto.Ngaytaotaikhoan;
            existing.Avatar = nhanvienDto.Avatar;

            // Nếu chỉ email trùng, cập nhật sdt mới
            if (checkEmail != null && checkSdt == null)
                existing.Sdt = nhanvienDto.Sdt;

            // Nếu chỉ sdt trùng, cập nhật email mới
            if (checkEmail == null && checkSdt != null)
                existing.Email = nhanvienDto.Email;

            await _repository.UpdateAsync(existing);
            return existing.Id;
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
        public async Task UpdateNhanvienAsync(int id, NhanvienUpdateDTO nhanvienDto)
        {
            var nhanvien = await _repository.GetByIdAsync(id);
            if (nhanvien == null) throw new KeyNotFoundException("Nhân viên không tồn tại.");

            nhanvien.Hovaten = nhanvienDto.Hovaten;
            nhanvien.Ngaysinh = nhanvienDto.Ngaysinh;
            nhanvien.Diachi = nhanvienDto.Diachi;
            nhanvien.Gioitinh = nhanvienDto.Gioitinh;
            nhanvien.Sdt = nhanvienDto.Sdt;
            nhanvien.Email = nhanvienDto.Email;
            nhanvien.Role = nhanvienDto.Role;
            nhanvien.Avatar = nhanvienDto.Avatar;
            await _repository.UpdateAsync(nhanvien);
        }

        public async Task UpdateThongTinNhanvienAsync(int id, NhanvienUpdateDTO nhanvienDto)
        {
            var nhanvien = await _repository.GetByIdAsync(id);
            if (nhanvien == null) throw new KeyNotFoundException("Nhân viên không tồn tại.");

            nhanvien.Hovaten = nhanvienDto.Hovaten;
            nhanvien.Ngaysinh = nhanvienDto.Ngaysinh;
            nhanvien.Diachi = nhanvienDto.Diachi;
            nhanvien.Gioitinh = nhanvienDto.Gioitinh;
            nhanvien.Sdt = nhanvienDto.Sdt;
            nhanvien.Email = nhanvienDto.Email;
            nhanvien.Avatar = nhanvienDto.Avatar;
            await _repository.UpdateAsync(nhanvien);
        }

        public async Task DeleteNhanvienAsync(int id)
        {
            var a = await _repository.GetByIdAsync(id);
            if (a == null) throw new KeyNotFoundException("Nhân viên không tồn tại.");
            a.Trangthai = 2;
            await _repository.UpdateAsync(a);
        }
		public Task<int> GetTongNhanVienTrangThai0Async()
	=> _repository.CountNhanVienTrangThai0Async();
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
                Avatar = n.Avatar,
                Ngaytaotaikhoan = n.Ngaytaotaikhoan
            });
        }
    }
}
