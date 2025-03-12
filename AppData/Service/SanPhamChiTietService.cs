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
        private readonly ISanPhamChiTietRepo _repository;
        private readonly ISanPhamRepo _isanphamchitietRepos;
        public SanPhamChiTietService(ISanPhamChiTietRepo repository, ISanPhamRepo isanphamchitietRepos)
        {
            _repository = repository;
            _isanphamchitietRepos = isanphamchitietRepos;

        }
        public async Task<IEnumerable<Sanphamchitiet>> GetAllAsync()
        {
            var sanphamchitiets = await _repository.GetAllAsync();

            return sanphamchitiets.Select(sanphamchitiet => new Sanphamchitiet
            {
                Id = sanphamchitiet.Id,
                Mota = sanphamchitiet.Mota,
                Trangthai = sanphamchitiet.Trangthai,
                Giathoidiemhientai = sanphamchitiet.Giathoidiemhientai,
                Soluong = sanphamchitiet.Soluong,
                Idsp = sanphamchitiet.Idsp,
            });
        }

        public async Task<Sanphamchitiet> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new Sanphamchitiet
            {
                Id = entity.Id,
                Mota = entity.Mota,
                Trangthai = entity.Trangthai,
                Giathoidiemhientai = entity.Giathoidiemhientai,
                Soluong = entity.Soluong,
                Idsp = entity.Idsp,
            };
        }

        public async Task<List<SanphamchitietDTO>> GetByIdSPAsync(int idspct)
        {
            try
            {
                // Gọi repository để lấy dữ liệu
                var results = await _repository.GetByIdSPAsync(idspct);

                if (results == null || !results.Any())
                    throw new KeyNotFoundException("Không tìm thấy sản phẩm trong sản phẩm chi tiết với ID: " + idspct);

                // Ánh xạ thủ công từ entity sang DTO
                var dtoList = results.Select(result => new SanphamchitietDTO
                {
                    Id = result.Id,
                    Mota = result.Mota,
                    Trangthai = result.Trangthai,
                    Giathoidiemhientai = result.Giathoidiemhientai,
                    Soluong = result.Soluong,
                    Idsp = result.Idsp,
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm thuộc tính sản phẩm chi tiết: " + ex.Message);
            }
        }

        public async Task AddAsync(SanphamchitietDTO dto)
        {
            var sanpham = await _isanphamchitietRepos.GetByIdAsync(dto.Idsp);
            if (sanpham == null) throw new ArgumentNullException("Sản phẩm không tồn tại");

            var sanphamchitiet = new Sanphamchitiet
            {
                Mota = dto.Mota,
                Trangthai = dto.Trangthai,
                Giathoidiemhientai = dto.Giathoidiemhientai,
                Soluong = dto.Soluong,
                Idsp = dto.Idsp,
            };

            await _repository.AddAsync(sanphamchitiet);
        }

        public async Task UpdateAsync(int id, SanphamchitietDTO dto)
        {

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Không tìm thấy sản phẩm chi tiết.");

            var sanpham = await _isanphamchitietRepos.GetByIdAsync(dto.Idsp);
            if (sanpham == null) throw new ArgumentNullException("Sản phẩm không tồn tại");

            if (entity != null)
            {
                entity.Mota = dto.Mota;
                entity.Trangthai = dto.Trangthai;
                entity.Giathoidiemhientai = dto.Giathoidiemhientai;
                entity.Soluong = dto.Soluong;
                entity.Idsp = dto.Idsp;

                await _repository.UpdateAsync(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
