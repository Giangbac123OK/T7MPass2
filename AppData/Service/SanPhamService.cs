﻿using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using AppData.ViewModel;
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
        private readonly ISanPhamRepo _repository;
        private readonly IHoaDonChiTietRepo _hoaDonChiTietRepo;

        public SanPhamService(ISanPhamRepo repository, IHoaDonChiTietRepo hoaDonChiTietRepo)
        {
            _repository = repository;
            _hoaDonChiTietRepo = hoaDonChiTietRepo;
        }

        public async Task<IEnumerable<Sanpham>> GetAllAsync()
        {
            return await _repository.GetAllAsync();

        }

        public async Task AddAsync(SanphamDTO sanphamDto)
        {
            var sanpham = new Sanpham
            {
                TenSanpham = sanphamDto.TenSanpham,
                Mota = sanphamDto.Mota,
                Soluong = sanphamDto.Soluong,
                GiaBan = sanphamDto.GiaBan,
                Chieudai = sanphamDto.Chieudai,
                Chieurong = sanphamDto.Chieurong,
                Trongluong = sanphamDto.Trongluong,
                //Giasale = sanphamDto.Giasale,
                Idth = sanphamDto.Idth,
                Trangthai = sanphamDto.Soluong > 0 ? 0 : 1
            };

            await _repository.AddAsync(sanpham);
        }

        public async Task UpdateAsync(int id, SanphamDTO sanphamDto)
        {
            var sanpham = await _repository.GetByIdAsync(id);
            if (sanpham == null) return;

            sanpham.TenSanpham = sanphamDto.TenSanpham;
            sanpham.Mota = sanphamDto.Mota;
            sanpham.Soluong = sanphamDto.Soluong;
            sanpham.GiaBan = sanphamDto.GiaBan;
            sanpham.Chieudai = sanphamDto.Chieudai;
            sanpham.Chieurong = sanphamDto.Chieurong;
            sanpham.Trongluong = sanphamDto.Trongluong;
            //sanpham.Giasale = sanphamDto.Giasale;
            sanpham.Idth = sanphamDto.Idth;
            sanpham.Trangthai = sanphamDto.Soluong > 0 ? 0 : 1;

            await _repository.UpdateAsync(sanpham);
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<IEnumerable<SanphamDTO>> SearchByNameAsync(string name)
        {
            var sanphams = await _repository.SearchByNameAsync(name);
            return sanphams.Select(sp => new SanphamDTO
            {
                Id = sp.Id,
                TenSanpham = sp.TenSanpham,
                Mota = sp.Mota,
                Trangthai = sp.Trangthai,
                Soluong = sp.Soluong,
                GiaBan = sp.GiaBan,
                //Giasale = sp.Giasale,
                Chieudai = sp.Chieudai,
                Chieurong = sp.Chieurong,
                Trongluong = sp.Trongluong,
                Idth = sp.Idth
            });
        }
        public async Task UpdateStatusLoad(int id)
        {

            var sale = await _repository.GetByIdAsync(id);
            if (sale == null)
            {
                throw new KeyNotFoundException("Sản phẩm không tồn tại");
            }
            if (sale.Trangthai != 3)
            {
                // Cập nhật trạng thái dựa trên ngày bắt đầu và ngày kết thúc
                if (sale.Soluong > 0)
                {
                    sale.Trangthai = 0; // Đang diễn ra
                }
                else if (sale.Soluong == 0)
                {
                    sale.Trangthai = 1; // Chuẩn bị diễn ra
                }
            }


            await _repository.UpdateAsync(sale);
        }
        public async Task UpdateStatusToCancelled(int id)
        {
            var sale = await _repository.GetByIdAsync(id);
            if (sale == null)
            {
                throw new KeyNotFoundException("Sản phẩm không tồn tại");
            }

            sale.Trangthai = 2; // Cập nhật trạng thái thành Hủy
            await _repository.UpdateAsync(sale);
        }
        public async Task<SanphamDTO> GetByIdAsync(int id)
        {
            var sanpham = await _repository.GetByIdAsync(id);

            if (sanpham == null)
                return null;

            // Chuyển đổi đối tượng Sanpham thành SanphamDTO
            return new SanphamDTO
            {
                TenSanpham = sanpham.TenSanpham,
                Mota = sanpham.Mota,
                Trangthai = sanpham.Trangthai,
                Soluong = sanpham.Soluong,
                GiaBan = sanpham.GiaBan,
                //Giasale = sanpham.Giasale,
                Chieudai = sanpham.Chieudai,
                Chieurong = sanpham.Chieurong,
                Trongluong = sanpham.Trongluong,
                Idth = sanpham.Idth
            };
        }

        public async Task<IEnumerable<SanphamViewModel>> GetAllSanphamViewModels()
        {
            return await _repository.GetAllSanphamViewModels();
        }

        public async Task<SanphamViewModel> GetAllSanphamViewModelsByIdSP(int idsp)
        {
            var spView= await _repository.GetSanphamViewModelByIdSP(idsp);
            foreach (var item in spView.Sanphamchitiets)
            {
                item.soLuongBan = await GetTotalSoldQuantityAsync(item.Id);
                
            }
            return spView;
        }

        public async Task<IEnumerable<SanphamViewModel>> GetAllSanphamGiamGiaViewModels()
        {
            return await _repository.GetAllSanphamGiamGiaViewModels();
        }

        public async Task<IEnumerable<SanphamViewModel>> GetAllSanphamByThuongHieu(int id)
        {
            return await _repository.GetAllSanphamByThuongHieu(id);
        }

        public async Task<IEnumerable<SanphamViewModel>> GetSanphamByThuocTinh( decimal? giaMin = null, decimal? giaMax = null, int? idThuongHieu = null)
        {
            return await _repository.GetSanphamByThuocTinh( giaMin, giaMax, idThuongHieu);
        }

        public async Task<int> GetTotalSoldQuantityAsync(int idSanphamChitiet)
        {
            var hoaDonChiTiets = await _hoaDonChiTietRepo.GetAllAsync();

            return hoaDonChiTiets
            .Where(hdct => hdct.Idspct == idSanphamChitiet &&
                     hdct.Hoadon.Trangthaidonhang == (int)OrderStatus.DonHangThanhCong)
            .Select(hdct => hdct.Soluong)
            .DefaultIfEmpty(0) 
            .Sum();

        }
    }
}
