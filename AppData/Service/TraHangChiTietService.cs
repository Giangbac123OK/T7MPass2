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
    public class TraHangChiTietService : ITraHangChiTietService

    {
        private readonly ITraHangChiTietRepo _repo;

        public TraHangChiTietService(ITraHangChiTietRepo repo)
        {
            _repo = repo;
        }

       
        public async Task Create(TrahangchitietDTO dto)
        {
            var item = new Trahangchitiet
            {
                Idhdct = dto.Idhdct,
                Idth = dto.Idth,
                Ghichu = dto.Ghichu,
                Soluong = dto.Soluong,
                Tinhtrang = dto.Tinhtrang,
                Hinhthucxuly = dto.Hinhthucxuly,

            };
            await _repo.Create(item);
        }

        public async Task Delete(int id)
        {
           await _repo.Delete(id);
        }

        public async Task<List<Trahangchitiet>> GetAll() => await _repo.GetAll();

        public async Task<Trahangchitiet> GetById(int id) => await _repo.GetById(id);

     

        public async Task Update(TrahangchitietDTO dto)
        {
            var item = await GetById(dto.Id);
            item.Idhdct = dto.Idhdct;
            item.Idth = dto.Idth;
            item.Ghichu = dto.Ghichu;
            item.Soluong = dto.Soluong;
            item.Tinhtrang = dto.Tinhtrang;
            item.Hinhthucxuly = dto.Hinhthucxuly;

            await _repo.Update(item);
        }
    }
}
