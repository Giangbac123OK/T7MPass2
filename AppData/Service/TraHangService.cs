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
    public class TraHangService : ITraHangService
    {
        private readonly ITraHangRepo _repo;

        public TraHangService(ITraHangRepo repo)
        {
            _repo = repo;
        }

        public async Task Create(TrahangDTO dto)
        {
            var item = new Trahang
            {
                Idnv = dto.Idnv,
                Idkh = dto.Idkh,
                Tenkhachhang = dto.Tenkhachhang,
                Sotienhoan = dto.Sotienhoan,
                Lydotrahang = dto.Lydotrahang,
                Trangthai = dto.Trangthai,
                Phuongthuchoantien = dto.Phuongthuchoantien,
                Ngaytrahangdukien = dto.Ngaytrahangdukien,
                Ngaytrahangthucte = dto.Ngaytrahangthucte,
                Chuthich = dto.Chuthich,
                Hinhthucxuly = dto.Hinhthucxuly,
            };
            await _repo.Create(item);                                                                               
        }

        public async Task Delete(int id)
        {
           await _repo.Delete(id);
        }

        public async Task<List<Trahang>> GetAll() => await _repo.GetAll();

        public async Task<Trahang> GetById(int id) => await _repo.GetById(id);

      

        public async Task Update(TrahangDTO dto)
        {
            var item = await _repo.GetById(dto.Id);

            item.Idnv = dto.Idnv;
            item.Idkh = dto.Idkh;
            item.Tenkhachhang = dto.Tenkhachhang;                
            item.Sotienhoan = dto.Sotienhoan;
            item.Lydotrahang = dto.Lydotrahang;
            item.Trangthai = dto.Trangthai;
            item.Phuongthuchoantien = dto.Phuongthuchoantien;
            item.Ngaytrahangdukien = dto.Ngaytrahangdukien;
            item.Ngaytrahangthucte = dto.Ngaytrahangthucte;
            item.Chuthich = dto.Chuthich;
            item.Hinhthucxuly = dto.Hinhthucxuly;

            await _repo.Update(item);
        }
    }
}
