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
    public class HoaDonChiTietService : IHoaDonChiTietService
    {
        private readonly IHoaDonChiTietRepo _repository;
        private readonly IHoaDonRepo _HDrepository;
        private readonly ISanPhamChiTietRepo _SPCTrepository;
        public HoaDonChiTietService(IHoaDonChiTietRepo repository, IHoaDonRepo HDrepository, ISanPhamChiTietRepo SPCTrepository)
        {
            _repository = repository;
            _HDrepository = HDrepository;
            _SPCTrepository = SPCTrepository;
        }

        public async Task Create(HoadonchitietDTO dto)
        {

            var hoadon = await _HDrepository.GetById(dto.Idhd);
            if (hoadon == null) return;

            var sanphamchitiet = await _SPCTrepository.GetById(dto.Idspct);
            if (sanphamchitiet == null) return;

            var hoadonchitiet = new Hoadonchitiet
            {
                Idhd = dto.Idhd,
                Idspct = dto.Idspct,
                Soluong = dto.Soluong,
                Giasp = dto.Giasp,
                Giamgia = dto.Giamgia,
            };

            await _repository.Create(hoadonchitiet);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Hoadonchitiet>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Hoadonchitiet> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(HoadonchitietDTO dto)
        {
            var hoadonchitiet = await _repository.GetById(dto.Id);
            if (hoadonchitiet == null) return;

            var hoadon = await _HDrepository.GetById(dto.Idhd);
            if (hoadon == null) return;

            var sanphamchitiet = await _SPCTrepository.GetById(dto.Idspct);
            if (sanphamchitiet == null) return;

            hoadonchitiet.Idhd = dto.Idhd;
            hoadonchitiet.Idspct = dto.Idspct;
            hoadonchitiet.Soluong = dto.Soluong;
            hoadonchitiet.Giasp = dto.Giasp;
            hoadonchitiet.Giamgia = dto.Giamgia;

            await _repository.Update(hoadonchitiet);
        }
    }
}
