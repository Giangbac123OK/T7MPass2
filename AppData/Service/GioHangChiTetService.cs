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
    public class GioHangChiTetService : IGioHangChiTetService
    {
        private readonly IGioHangChiTetRepo _repository;
        private readonly IGioHangRepo _GHrepository;
        private readonly ISanPhamChiTietRepo _SPCTrepository;
        public GioHangChiTetService(IGioHangChiTetRepo repository, IGioHangRepo GHrepository, ISanPhamChiTietRepo SPCTrepository)
        {
            _repository = repository;
            _GHrepository = GHrepository;
            _SPCTrepository = SPCTrepository;
        }

        public async Task Create(GiohangchitietDTO dto)
        {
            var giohang = await _GHrepository.GetById(dto.Idgh);
            if (giohang == null) return;

            var giohangchitiet = new Giohangchitiet
            {
                Idgh = dto.Idgh,
                Idspct = dto.Idspct,
                Soluong = dto.Soluong
            };

            await _repository.Create(giohangchitiet);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Giohangchitiet>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Giohangchitiet> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(GiohangchitietDTO dto)
        {
            var giohangchitiet = await _repository.GetById(dto.Id);
            if (giohangchitiet == null) return;

            var giohang = await _GHrepository.GetById(dto.Idgh);
            if (giohang == null) return;

            var sanphamchitiet = await _SPCTrepository.GetById(dto.Idspct);
            if (sanphamchitiet == null) return;

            giohangchitiet.Idgh = dto.Idgh;
            giohangchitiet.Idspct = dto.Idspct;
            giohangchitiet.Soluong = dto.Soluong;

            await _repository.Update(giohangchitiet);
        }
    }
}
