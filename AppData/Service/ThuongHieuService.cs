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
    public class ThuongHieuService : IThuongHieuService
    {
        private readonly IThuongHieuRepo _repo;

        public ThuongHieuService(IThuongHieuRepo repo)
        {
            _repo = repo;
        }


        public async Task Create(ThuonghieuDTO dto)
        {
            var item = new Thuonghieu
            {
                Tenthuonghieu = dto.Tenthuonghieu,
                Tinhtrang = dto.Tinhtrang,
            };
            await _repo.Create(item);
        }

        public async Task Delete(int id)
        {
         await _repo.Delete(id);
        }

        public async Task<List<Thuonghieu>> GetAll() => await _repo.GetAll();

        public async Task<Thuonghieu> GetById(int id) => await  _repo.GetById(id);

        public async Task Update(ThuonghieuDTO dto)
        {
            var item = await _repo.GetById(dto.Id);
            item.Tenthuonghieu = dto.Tenthuonghieu;
            item.Tinhtrang = dto.Tinhtrang;
            await _repo.Update(item);
        }
    }
}
