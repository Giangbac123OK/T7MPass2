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
    public class HoaDonService : IHoaDonService
    {
        private readonly IHoaDonRepo _repository;
        private readonly INhanVienRepo _NVrepository;
        private readonly IKhachHangRepo _KHrepository;
        private readonly IGiamGiaRepo _GGrepository;
        private readonly IPhuongThucThanhToanRepo _PTTTrepository;
        public HoaDonService(IHoaDonRepo repository, INhanVienRepo NVrepository, IKhachHangRepo KHrepository, IGiamGiaRepo GGrepository, IPhuongThucThanhToanRepo PTTTrepository)
        {
            _repository = repository;
            _NVrepository = NVrepository;
            _KHrepository = KHrepository;
            _GGrepository = GGrepository;
            _PTTTrepository = PTTTrepository;
        }

        public async Task UpdateTrangThaiAsync(int orderCode, int status, int trangthaiTT)
        {
            var entity = await _repository.GetByIdAsync(orderCode);
            if (entity == null) throw new KeyNotFoundException("Hoá đơn không tồn tại");

            entity.Trangthai = status;
            entity.Trangthaithanhtoan = trangthaiTT;
            await _repository.UpdateAsync(entity);
        }


        public async Task<IEnumerable<Hoadon>> GetAllAsync()
        {

            var entities = await _repository.GetAllAsync();

            return entities.Select(hoaDon => new Hoadon
            {
                Id = hoaDon.Id,
                Idnv = hoaDon.Idnv,
                Idkh = hoaDon.Idkh,
                Trangthaithanhtoan = hoaDon.Trangthaithanhtoan,
                Trangthaidonhang = hoaDon.Trangthaidonhang,
                Thoigiandathang = hoaDon.Thoigiandathang,
                Diachiship = hoaDon.Diachiship,
                Ngaygiaothucte = hoaDon.Ngaygiaothucte,
                Tongtiencantra = hoaDon.Tongtiencantra,
                Tongtiensanpham = hoaDon.Tongtiensanpham,
                Sdt = hoaDon.Sdt,
                Tonggiamgia = hoaDon.Tonggiamgia,
                Ghichu = hoaDon.Ghichu,
                Idgg = hoaDon.Idgg,
                Trangthai = hoaDon.Trangthai,
            });
        }

        public async Task<List<HoadonDTO>> Checkvoucher(int idKh)
        {
            try
            {
                // Lấy dữ liệu từ repository
                var results = await _repository.Checkvoucher(idKh);

                // Kiểm tra nếu không có dữ liệu hoặc dữ liệu không hợp lệ, trả về null
                if (results == null || !results.Any())
                    return null; // Trả về null khi không có dữ liệu

                // Ánh xạ thủ công từ entity sang DTO và lọc chỉ lấy những Idgg khác null
                var dtoList = results
                    .Where(result => result.Idgg != null)  // Lọc các Idgg không null
                    .Select(result => new HoadonDTO
                    {
                        Idgg = result.Idgg,
                    })
                    .ToList();

                // Nếu không có mã giảm giá hợp lệ, trả về null
                if (!dtoList.Any())
                    return null;

                // Trả về danh sách DTO
                return dtoList;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và throw ra thông báo lỗi
                throw new Exception("Lỗi khi tìm giảm giá trong hoá đơn: " + ex.Message);
            }
        }

        public async Task<HoadonDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new HoadonDTO
            {
                Id = entity.Id,
                Idnv = entity.Idnv,
                Idkh = entity.Idkh,
                Trangthaithanhtoan = entity.Trangthaithanhtoan,
                Trangthaidonhang = entity.Trangthaidonhang,
                Thoigiandathang = entity.Thoigiandathang,
                Diachiship = entity.Diachiship,
                Ngaygiaothucte = entity.Ngaygiaothucte,
                Tongtiencantra = entity.Tongtiencantra,
                Tongtiensanpham = entity.Tongtiensanpham,
                Sdt = entity.Sdt,
                Tonggiamgia = entity.Tonggiamgia,
                Ghichu = entity.Ghichu,
                Idgg = entity.Idgg,
                Trangthai = entity.Trangthai,
            };
        }

        public async Task AddAsync(HoadonDTO HoadonDTO)
        {
            // Kiểm tra xem khách hàng có tồn tại không
            if (HoadonDTO.Idkh != 0)
            {
                int idkh = HoadonDTO.Idkh.Value;
                var khachhang = await _KHrepository.GetByIdAsync(idkh);
                if (khachhang == null) throw new ArgumentNullException("Khách hàng không tồn tại");
            }

            // Kiểm tra xem mã giảm giá có tồn tại không
            if (HoadonDTO.Idgg != 0)
            {
                int idgg = HoadonDTO.Idgg.Value; // Chuyển đổi từ int? sang int
                var giamgia = await _GGrepository.GetByIdAsync(idgg);

                if (giamgia == null)
                {
                    // Cải thiện thông báo lỗi nếu không tìm thấy mã giảm giá
                    throw new ArgumentNullException(nameof(giamgia), "Mã giảm giá không tồn tại");
                }

                // Giảm số lượng mã giảm giá
                giamgia.Soluong -= 1;
                _GGrepository.UpdateAsync(giamgia);
            }

            // Tạo đối tượng Hoadon từ DTO
            var hoaDon = new Hoadon
            {
                Idnv = HoadonDTO.Idnv == 0 ? (int?)null : HoadonDTO.Idnv,  // Nếu Idnv = 0, gán null
                Idkh = HoadonDTO.Idkh == 0 ? (int?)null : HoadonDTO.Idkh,  // Nếu Idgg = 0, gán null
                Trangthaithanhtoan = HoadonDTO.Trangthaithanhtoan,
                Trangthaidonhang = HoadonDTO.Trangthaidonhang,
                Thoigiandathang = HoadonDTO.Thoigiandathang,
                Diachiship = HoadonDTO.Diachiship,
                Ngaygiaothucte = null,
                Tongtiencantra = HoadonDTO.Tongtiencantra,
                Tongtiensanpham = HoadonDTO.Tongtiensanpham,
                Ghichu = HoadonDTO.Ghichu,
                Sdt = HoadonDTO.Sdt,
                Tonggiamgia = HoadonDTO.Tonggiamgia,
                Idgg = HoadonDTO.Idgg == 0 ? (int?)null : HoadonDTO.Idgg,  // Nếu Idgg = 0, gán null
                Trangthai = HoadonDTO.Trangthai,
            };

            // Thêm hóa đơn vào cơ sở dữ liệu
            await _repository.AddAsync(hoaDon);

            // Gán lại ID của hóa đơn từ đối tượng Hoadon vào DTO
            HoadonDTO.Id = hoaDon.Id; // Gán ID từ Hoadon vào DTO
        }

        // Phương thức cập nhật hoá đơn
        public async Task UpdateAsync(HoadonDTO dto, int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Hóa đơn không tồn tại");

            // Kiểm tra xem khách hàng có tồn tại không
            if (dto.Idkh != 0)
            {
                int idkh = dto.Idkh.Value;
                var khachhang = await _KHrepository.GetByIdAsync(idkh);
                if (khachhang == null) throw new ArgumentNullException("Khách hàng không tồn tại");
            }

            if (entity != null)
            {
                entity.Idnv = dto.Idnv == 0 ? (int?)null : dto.Idnv;
                entity.Idkh = dto.Idkh == 0 ? (int?)null : dto.Idkh;
                entity.Trangthaithanhtoan = dto.Trangthaithanhtoan;
                entity.Trangthaidonhang = dto.Trangthaidonhang;
                entity.Thoigiandathang = dto.Thoigiandathang;
                entity.Diachiship = dto.Diachiship;
                entity.Ngaygiaothucte = dto.Ngaygiaothucte;
                entity.Tongtiencantra = dto.Tongtiencantra;
                entity.Tongtiensanpham = dto.Tongtiensanpham;
                entity.Sdt = dto.Sdt;
                entity.Tonggiamgia = dto.Tonggiamgia;
                entity.Idgg = dto.Idgg == 0 ? (int?)null : dto.Idgg;
                entity.Trangthai = dto.Trangthai;
                entity.Ghichu = dto.Ghichu;
                await _repository.UpdateAsync(entity);

                entity.Id = id; // Gán ID từ Hoadon vào DTO
            }
        }

        // Phương thức xóa hoá đơn
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<HoaDonViewModel>> TimhoadontheoIdKH(int id)
        {
            return await _repository.TimhoadontheoIdKH(id);
        }

        public async Task Danhandonhang(int id)
        {
            await _repository.Danhandonhang(id);
        }
    }
}
