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
    public class PhuongThucThanhToanService : IPhuongThucThanhToanService
    {
        private readonly IPhuongThucThanhToanRepo _repository;
        public PhuongThucThanhToanService(IPhuongThucThanhToanRepo repository)
        {
            _repository = repository;

        }

        public async Task Create(PhuongthucthanhtoanDTO dto)
        {
            var phuongthucthanhtoan = new Phuongthucthanhtoan
            {
                Tenpttt = dto.Tenpttt,
                Trangthai = dto.Trangthai
            };

            await _repository.Create(phuongthucthanhtoan);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Phuongthucthanhtoan>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Phuongthucthanhtoan> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(PhuongthucthanhtoanDTO dto)
        {
            var phuongthucthanhtoan = await _repository.GetById(dto.Id);
            if (phuongthucthanhtoan == null) return;

            phuongthucthanhtoan.Tenpttt = dto.Tenpttt;
            phuongthucthanhtoan.Trangthai = dto.Trangthai;

            await _repository.Update(phuongthucthanhtoan);
        }
    }
}
