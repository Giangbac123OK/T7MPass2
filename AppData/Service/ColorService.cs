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

        public ColorService(IColorRepo repos)
        {
            _repository = repos;
        }


        public async Task<IEnumerable<ColorDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new ColorDTO
            {
                Id = e.Id,
                Tenmau = e.Tenmau,
                Mamau = e.Mamau,
                Trangthai = e.Trangthai
            });
        }

        public async Task<ColorDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new ColorDTO
            {
                Id = entity.Id,
                Tenmau = entity.Tenmau,
                Mamau = entity.Mamau,
                Trangthai = entity.Trangthai
            };
        }

        public async Task<ColorDTO> AddAsync(ColorDTO dto)
        {
            var entity = new Models.Color
            {
                Tenmau = dto.Tenmau,
                Mamau = dto.Mamau,
                Trangthai = dto.Trangthai
            };

            var addedEntity = await _repository.AddAsync(entity);
            return new ColorDTO
            {
                Tenmau = addedEntity.Tenmau,
                Mamau = addedEntity.Mamau,
                Trangthai = addedEntity.Trangthai
            };
        }

        public async Task<ColorDTO> UpdateAsync(int id, ColorDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            entity.Tenmau = dto.Tenmau;
            entity.Mamau = dto.Mamau;
            entity.Trangthai = dto.Trangthai;

            var updatedEntity = await _repository.UpdateAsync(entity);
            return new ColorDTO
            {
                Tenmau = updatedEntity.Tenmau,
                Mamau = updatedEntity.Mamau,
                Trangthai = updatedEntity.Trangthai
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
