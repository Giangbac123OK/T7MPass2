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
    public class SizeService : ISizeService
    {
        private readonly ISizeRepo _repo;

        public SizeService(ISizeRepo repo)
        {
            _repo = repo;
        }

        public async Task Create(SizeDTO dto)
        {
            var item = new Models.Size
            {
                Sosize = dto.Sosize,
                Trangthai = dto.Trangthai,
            };
            await _repo.Create(item);
        }

        public async Task Delete(int id)
        {
           await _repo.Delete(id);
        }

        public async Task<List<Models.Size>> GetAll() => await _repo.GetAll();

        public async Task<Models.Size> GetById(int id) => await _repo.GetById(id);

     

        public async Task Update(SizeDTO dto)
        {
           var item = await _repo.GetById(dto.Id);
            item.Sosize = dto.Sosize;
            item.Trangthai = dto.Trangthai;
            await _repo.Update(item);
                                                                
        }
    }
}
