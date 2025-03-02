using AppData.DTO;
using AppData.IRepository;
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
    public class KhachHangService : IKhachHangService
    {
        private readonly IKhachHangRepo _repository;
        private readonly IRankRepo _Rankrepository;
        public KhachHangService(IKhachHangRepo repository, IRankRepo rankrepository)
        {
            _repository = repository;
            _Rankrepository = rankrepository;
        }

        public async Task Create(KhachhangDTO dto)
        {
            var rank = await _Rankrepository.GetById(dto.Idrank);
            if (rank == null) return;

            var khachhang = new Khachhang
            {
                Ten = dto.Ten,
                Sdt = dto.Sdt,
                Ngaysinh = dto.Ngaysinh,
                Tichdiem = dto.Tichdiem,
                Email = dto.Email,
                Diachi = dto.Diachi,
                Password = dto.Password,
                Ngaytaotaikhoan = dto.Ngaytaotaikhoan,
                Diemsudung = dto.Diemsudung,
                Trangthai = dto.Trangthai,
                Idrank = dto.Idrank,
                Gioitinh = dto.Gioitinh
            };

            await _repository.Create(khachhang);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Khachhang>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Khachhang> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(KhachhangDTO dto)
        {
            var khachhang = await _repository.GetById(dto.Id);
            if (khachhang == null) return;

            var rank = await _Rankrepository.GetById(dto.Idrank);
            if (rank == null) return;

            khachhang.Ten = dto.Ten;
            khachhang.Sdt = dto.Sdt;
            khachhang.Ngaysinh = dto.Ngaysinh;
            khachhang.Tichdiem = dto.Tichdiem;
            khachhang.Email = dto.Email;
            khachhang.Diachi = dto.Diachi;
            khachhang.Password = dto.Password;
            khachhang.Ngaytaotaikhoan = dto.Ngaytaotaikhoan;
            khachhang.Diemsudung = dto.Diemsudung;
            khachhang.Trangthai = dto.Trangthai;
            khachhang.Idrank = dto.Idrank;
            khachhang.Trangthai = dto.Trangthai;

            await _repository.Update(khachhang);
        }
    }
}
