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
    public class SanPhamService : ISanPhamService
    {

        private readonly ISanPhamRepo _repo;

        public SanPhamService(ISanPhamRepo repo)
        {
            _repo = repo;
        }

        public async Task Create(SanphamDTO dto)
        {
            var item = new Sanpham
            {
                TenSanpham = dto.TenSanpham,
                Mota = dto.Mota,
                Trangthai = dto.Trangthai,
                Soluong = dto.Soluong,
                GiaBan = dto.GiaBan,
                NgayThemMoi = dto.NgayThemMoi,
                Chieudai = dto.Chieudai,
                Chieurong = dto.Chieurong,
                Trongluong = dto.Trongluong,
                Idth = dto.Idth,
            };
            await _repo.Create(item);
        }

        public async Task Delete(int id)
        {
           await _repo.Delete(id);
        }

        public async Task<List<Sanpham>> GetAll() => await _repo.GetAll();

        public async Task<Sanpham> GetById(int id) => await _repo.GetById(id);

        public async Task<List<SanphamDTO>> SpNoiBat()
        {
            var data = await _repo.SpNoiBat();
            return data.Select(g => new SanphamDTO()
            {
                Id = g.Id,
                TenSanpham = g.TenSanpham,
                Trangthai = g.Trangthai,
                Soluong = g.Soluong,
                GiaBan = g.GiaBan,
                Idth = g.Idth,
                Mota = g.Mota,
                NgayThemMoi = g.NgayThemMoi
            }).ToList();
        }
        public async Task<List<SanphamDTO>> SpMoiNhat()
        {
            var data = await _repo.SpMoiNhat();
            return data.Select(g => new SanphamDTO()
            {
                Id = g.Id,
                TenSanpham = g.TenSanpham,
                Trangthai = g.Trangthai,
                Soluong = g.Soluong,
                GiaBan = g.GiaBan,
                Idth = g.Idth,
                Mota = g.Mota,
                NgayThemMoi = g.NgayThemMoi
            }).ToList();
        }
        public async Task Update(SanphamDTO dto)
        {
            var item = await _repo.GetById(dto.Id);

            item.TenSanpham = dto.TenSanpham;
            item.Mota = dto.Mota;
            item.Trangthai = dto.Trangthai;
            item.Soluong = dto.Soluong;
            item.GiaBan = dto.GiaBan;
            item.NgayThemMoi = dto.NgayThemMoi;
            item.Chieudai = dto.Chieudai;
            item.Chieurong = dto.Chieurong;
            item.Trongluong = dto.Trongluong;
            item.Idth = dto.Idth;

            await _repo.Update(item);
        }
    }
}
