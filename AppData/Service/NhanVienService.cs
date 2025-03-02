using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        public async Task Create(NhanvienDTO dto)
        {
            var nhanvien = new Nhanvien
            {
                Hovaten = dto.Hovaten,
                Ngaysinh = dto.Ngaysinh,
                Diachi = dto.Diachi,
                Gioitinh = dto.Gioitinh,
                Sdt = dto.Sdt,
                Email = dto.Email,
                Trangthai = dto.Trangthai,
                Password = dto.Password,
                Role = dto.Role,
                Ngaytaotaikhoan = dto.Ngaytaotaikhoan
            };

            await _repository.Create(nhanvien);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Nhanvien>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Nhanvien> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(NhanvienDTO dto)
        {
            var nhanvien = await _repository.GetById(dto.Id);
            if (nhanvien == null) return;

            nhanvien.Hovaten = dto.Hovaten;
            nhanvien.Ngaysinh = dto.Ngaysinh;
            nhanvien.Diachi = dto.Diachi;
            nhanvien.Gioitinh = dto.Gioitinh;
            nhanvien.Sdt = dto.Sdt;
            nhanvien.Email = dto.Email;
            nhanvien.Trangthai = dto.Trangthai;
            nhanvien.Password = dto.Password;
            nhanvien.Role = dto.Role;
            nhanvien.Ngaytaotaikhoan = dto.Ngaytaotaikhoan;

            await _repository.Update(nhanvien);
        }
    }
}
