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
                UrlHinhanh = sanphamchitiet.UrlHinhanh,
                Trangthai = sanphamchitiet.Trangthai,
                Giathoidiemhientai = sanphamchitiet.Giathoidiemhientai,
                Soluong = sanphamchitiet.Soluong,
                Idsp = sanphamchitiet.Idsp,
                IdSize = sanphamchitiet.IdSize,
                IdMau = sanphamchitiet.IdMau,
                IdChatLieu = sanphamchitiet.IdChatLieu,
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
                UrlHinhanh = entity.UrlHinhanh,
                Giathoidiemhientai = entity.Giathoidiemhientai,
                Soluong = entity.Soluong,
                Idsp = entity.Idsp,
                IdSize = entity.IdSize,
                IdMau = entity.IdMau,
                IdChatLieu = entity.IdChatLieu,
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
                    UrlHinhanh = result.UrlHinhanh,
                    Soluong = result.Soluong,
                    Idsp = result.Idsp,
                    IdSize = result.IdSize,
                    IdMau = result.IdMau,
                    IdChatLieu = result.IdChatLieu,
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
            var size = await _isanphamchitietRepos.GetByIdAsync(dto.IdSize);
            if (size == null) throw new ArgumentNullException("Size không tồn tại");

            var chatlieu = await _isanphamchitietRepos.GetByIdAsync(dto.IdChatLieu);
            if (chatlieu == null) throw new ArgumentNullException("Chất Liệu không tồn tại");

            var mau = await _isanphamchitietRepos.GetByIdAsync(dto.IdMau);
            if (mau == null) throw new ArgumentNullException("Màu không tồn tại");

            var existingSanphamchitiet = await _repository.GetAllAsync();
            bool exists = existingSanphamchitiet.Any(spct =>
                spct.Idsp == dto.Idsp &&
                spct.IdSize == dto.IdSize &&
                spct.IdMau == dto.IdMau &&
                spct.IdChatLieu == dto.IdChatLieu
            );

            if (exists)
            {
                throw new InvalidOperationException("Sản phẩm chi tiết với màu, size, chất liệu này đã tồn tại");
            }
            var sanphamchitiet = new Sanphamchitiet
            {
                Mota = dto.Mota,
                Trangthai = dto.Trangthai,
                Giathoidiemhientai = dto.Giathoidiemhientai,
                UrlHinhanh = dto.UrlHinhanh,
                Soluong = dto.Soluong,
                Idsp = dto.Idsp,
                IdSize = dto.IdSize,
                IdMau = dto.IdMau,
                IdChatLieu = dto.IdChatLieu,
            };

            await _repository.AddAsync(sanphamchitiet);
        }

        public async Task UpdateAsync(int id, SanphamchitietDTO dto)
        {


            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Không tìm thấy sản phẩm chi tiết.");

            var sanpham = await _isanphamchitietRepos.GetByIdAsync(dto.Idsp);
            if (sanpham == null) throw new ArgumentNullException("Sản phẩm không tồn tại");
            var size = await _isanphamchitietRepos.GetByIdAsync(dto.IdSize);
            if (size == null) throw new ArgumentNullException("Size không tồn tại");

            var chatlieu = await _isanphamchitietRepos.GetByIdAsync(dto.IdChatLieu);
            if (chatlieu == null) throw new ArgumentNullException("Chất Liệu không tồn tại");

            var mau = await _isanphamchitietRepos.GetByIdAsync(dto.IdMau);
            if (mau == null) throw new ArgumentNullException("Màu không tồn tại");


            var existingSanphamchitiet = await _repository.GetAllAsync();
            bool exists = existingSanphamchitiet.Any(spct =>
                spct.Idsp == dto.Idsp &&
                spct.IdSize == dto.IdSize &&
                spct.IdMau == dto.IdMau &&
                spct.IdChatLieu == dto.IdChatLieu
            );

            if (exists)
            {
                throw new InvalidOperationException("Sản phẩm chi tiết với màu, size, chất liệu này đã tồn tại");
            }

            if (entity != null)
            {
                entity.Mota = dto.Mota;
                entity.Trangthai = dto.Trangthai;
                entity.Giathoidiemhientai = dto.Giathoidiemhientai;
                entity.UrlHinhanh = dto.UrlHinhanh;
                entity.Soluong = dto.Soluong;
                entity.Idsp = dto.Idsp;
                entity.IdSize = dto.IdSize;
                entity.IdMau = dto.IdMau;
                entity.IdChatLieu = dto.IdChatLieu;

                await _repository.UpdateAsync(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
