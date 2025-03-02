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
    public class SaleChiTietService : ISaleChiTietService
    {
        private readonly ISaleChiTietRepo _repo;

        public SaleChiTietService(ISaleChiTietRepo repo)
        {
            _repo = repo;
        }

        public async Task Create(SalechitietDTO salechitiet)
        {
            var item = new Salechitiet
            {
                Idspct = salechitiet.Idspct,
                Idsale = salechitiet.Idsale,
                Donvi = salechitiet.Donvi,
                Soluong = salechitiet.Soluong,
                Giatrigiam = salechitiet.Giatrigiam,

            };
            await _repo.Create(item);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<Salechitiet>> GetAll() => await _repo.GetAll();

        public async Task<Salechitiet> GetById(int id) => await _repo.GetById(id);

        public async Task Update(SalechitietDTO dto)
        {
            var item = await _repo.GetById(dto.Id);

            item.Soluong = dto.Soluong;
            item.Idsale = dto.Idsale;
            item.Donvi = dto.Donvi;
            item.Soluong = dto.Soluong;
            item.Giatrigiam = dto.Giatrigiam;

            await _repo.Update(item);
            
        }
    }
}
