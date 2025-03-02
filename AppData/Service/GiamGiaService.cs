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
    public class GiamGiaService : IGiamGiaService
    {
        private readonly IGiamGiaRepo _repository;
        public GiamGiaService(IGiamGiaRepo repository)
        {
            _repository = repository;

        }

        public async Task Create(GiamgiaDTO dto)
        {
            var giamgia = new Giamgia
            {
                Mota = dto.Mota,
                Donvi = dto.Donvi,
                Soluong = dto.Soluong,
                Giatri = dto.Giatri,
                Ngaybatdau = dto.Ngaybatdau,
                Ngayketthuc = dto.Ngayketthuc,
                Trangthai = dto.Trangthai
            };

            await _repository.Create(giamgia);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Giamgia>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Giamgia> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(GiamgiaDTO dto)
        {
            var giamgia = await _repository.GetById(dto.Id);
            if (giamgia == null) return;

            giamgia.Mota = dto.Mota;
            giamgia.Donvi = dto.Donvi;
            giamgia.Soluong = dto.Soluong;
            giamgia.Giatri = dto.Giatri;
            giamgia.Ngaybatdau = dto.Ngaybatdau;
            giamgia.Ngayketthuc = dto.Ngayketthuc;
            giamgia.Trangthai = dto.Trangthai;

            await _repository.Update(giamgia);
        }
    }
}
