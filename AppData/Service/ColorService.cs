using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
    public class ColorService : IColorService
    {
        private readonly IColorRepo _repository;
        public ColorService(IColorRepo repository)
        {
            _repository = repository;

        }

        public async Task Create(ColorDTO dto)
        {
            var color = new Color
            {
                Tenmau = dto.Tenmau,
                Mamau = dto.Mamau,
                Trangthai = dto.Trangthai
            };

            await _repository.Create(color);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Color>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Color> GetById(int id)
        {
            return await _repository.GetById(id);  
        }

        public async Task Update(ColorDTO dto)
        {
            var color = await _repository.GetById(dto.Id);
            if (color == null) return;

            color.Tenmau = dto.Tenmau;
            color.Mamau = dto.Mamau;
            color.Trangthai = dto.Trangthai;

            await _repository.Update(color);
        }
    }
}
