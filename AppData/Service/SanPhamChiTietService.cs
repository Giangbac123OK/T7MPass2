using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
    public class SanPhamChiTietService : ISanPhamChiTietService
    {
        private readonly ISanPhamChiTietRepo _repo;

        public SanPhamChiTietService(ISanPhamChiTietRepo repo)
        {
            _repo = repo;
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Sanphamchitiet>> GetAll() => await _repo.GetAll();

        public async Task<Sanphamchitiet> GetById(int id) => await _repo.GetById(id);

        public async Task Create(SanphamchitietDTO dto)
        {
            var item = new Sanphamchitiet
            {
                Mota = dto.Mota,
                Trangthai = dto.Trangthai,
                Giathoidiemhientai = dto.Giathoidiemhientai,
                Soluong = dto.Soluong,
                UrlHinhanh = dto.UrlHinhanh,
                Idsp = dto.Idsp,
                IdChatLieu = dto.IdChatLieu,
                IdMau = dto.IdMau,
                IdSize = dto.IdSize,

            };
            await _repo.Create(item);
        }

        public async Task Update(SanphamchitietDTO dto)
        {
            var item = await _repo.GetById(dto.Id);
            item.Mota = dto.Mota;
            item.Trangthai = dto.Trangthai;
            item.Giathoidiemhientai = dto.Giathoidiemhientai;
            item.Soluong = dto.Soluong;
            item.UrlHinhanh = dto.UrlHinhanh;
            item.Idsp = dto.Idsp;
            item.IdChatLieu = dto.IdChatLieu;
            item.IdMau = dto.IdMau;
            item.IdSize = dto.IdSize;
            await _repo.Update(item);
        }
    }
}
