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
    public class GioHangService : IGioHangService
    {
        private readonly IGioHangRepo _repository;
        private readonly IKhachHangRepo _KHrepository;

        public GioHangService(IGioHangRepo repository, IKhachHangRepo KHrepository)
        {
            _repository = repository;
            _KHrepository = KHrepository;

        }

        public async Task Create(GiohangDTO dto)
        {
            var khachhang = await _KHrepository.GetById(dto.Idkh);
            if (khachhang == null) return;

            var giohang = new Giohang
            {
                Soluong = dto.Soluong,
                Idkh = dto.Idkh
            };

            await _repository.Create(giohang);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Giohang>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Giohang> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(GiohangDTO dto)
        {
            var giohang = await _repository.GetById(dto.id);
            if (giohang == null) return;

            var khachhang = await _KHrepository.GetById(dto.Idkh);
            if (khachhang == null) return;

            giohang.Soluong = dto.Soluong;
            giohang.Idkh = dto.Idkh;

            await _repository.Update(giohang);
        }
    }
}
