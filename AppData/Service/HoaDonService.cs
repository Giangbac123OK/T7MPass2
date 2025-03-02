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

        public async Task Create(HoadonDTO dto)
        {
            if (dto.Idnv != null)
            {
                int idnv = (int)dto.Idnv;
                var nhanvien = await _NVrepository.GetById(idnv);
                if (nhanvien == null) return;
            }

            if (dto.Idkh != null)
            {
                int idkh = (int)dto.Idkh;
                var nhanvien = await _NVrepository.GetById(idkh);
                if (nhanvien == null) return;
            }

            if (dto.Idgg != null)
            {
                int idgg = (int)dto.Idgg;
                var nhanvien = await _NVrepository.GetById(idgg);
                if (nhanvien == null) return;
            }

            var phuongthucthanhtoan = await _NVrepository.GetById(dto.Idpttt);
            if (phuongthucthanhtoan == null) return;

            var hoadon = new Hoadon
            {
                Idnv = dto.Idnv,
                Idkh = dto.Idkh,
                Trangthaithanhtoan = dto.Trangthaithanhtoan,
                Trangthaidonhang = dto.Trangthaidonhang,
                Thoigiandathang = dto.Thoigiandathang,
                Diachiship = dto.Diachiship,
                Ngaygiaothucte = dto.Ngaygiaothucte,
                Tongtiencantra = dto.Tongtiencantra,
                Tongtiensanpham = dto.Tongtiensanpham,
                Sdt = dto.Sdt,
                Tonggiamgia = dto.Tonggiamgia,
                Idgg = dto.Idgg,
                Trangthai = dto.Trangthai,
                Phivanchuyen = dto.Phivanchuyen,
                Idpttt = dto.Idpttt,
                Ghichu = dto.Ghichu
            };

            await _repository.Create(hoadon);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Hoadon>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Hoadon> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(HoadonDTO dto)
        {
            var hoadon = await _repository.GetById(dto.Id);
            if (hoadon == null) return;

            if (dto.Idnv != null)
            {
                int idnv = (int)dto.Idnv;
                var nhanvien = await _NVrepository.GetById(idnv);
                if (nhanvien == null) return;
            }

            if (dto.Idkh != null)
            {
                int idkh = (int)dto.Idkh;
                var nhanvien = await _NVrepository.GetById(idkh);
                if (nhanvien == null) return;
            }

            if (dto.Idgg != null)
            {
                int idgg = (int)dto.Idgg;
                var nhanvien = await _NVrepository.GetById(idgg);
                if (nhanvien == null) return;
            }

            var phuongthucthanhtoan = await _NVrepository.GetById(dto.Idpttt);
            if (phuongthucthanhtoan == null) return;

            hoadon.Idnv = dto.Idnv;
            hoadon.Idkh = dto.Idkh;
            hoadon.Trangthaithanhtoan = dto.Trangthaithanhtoan;
            hoadon.Trangthaidonhang = dto.Trangthaidonhang;
            hoadon.Thoigiandathang = dto.Thoigiandathang;
            hoadon.Diachiship = dto.Diachiship;
            hoadon.Ngaygiaothucte = dto.Ngaygiaothucte;
            hoadon.Tongtiencantra = dto.Tongtiencantra;
            hoadon.Tongtiensanpham = dto.Tongtiensanpham;
            hoadon.Sdt = dto.Sdt;
            hoadon.Tonggiamgia = dto.Tonggiamgia;
            hoadon.Idgg = dto.Idgg;
            hoadon.Trangthai = dto.Trangthai;
            hoadon.Phivanchuyen = dto.Phivanchuyen;
            hoadon.Idpttt = dto.Idpttt;
            hoadon.Ghichu = dto.Ghichu;

            await _repository.Update(hoadon);
        }
    }
}
