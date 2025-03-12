using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using AppData.ViewModel;
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
        private readonly ISizeRepo _repository;

        public SizeService(ISizeRepo repos)
        {
            _repository = repos;
        }


        public async Task<IEnumerable<SizeDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new SizeDTO
            {
                Sosize = e.Sosize,
                Trangthai = e.Trangthai
            });
        }

        public async Task<SizeDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new SizeDTO
            {
                Sosize = entity.Sosize,
                Trangthai = entity.Trangthai
            };
        }

        public async Task<SizeDTO> AddAsync(SizeDTO dto)
        {
            var entity = new Models.Size
            {
                Sosize = dto.Sosize,
                Trangthai = dto.Trangthai
            };

            var addedEntity = await _repository.AddAsync(entity);
            return new SizeDTO
            {
                Sosize = addedEntity.Sosize,
                Trangthai = addedEntity.Trangthai
            };
        }

        public async Task<SizeDTO> UpdateAsync(int id, SizeDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            entity.Sosize = dto.Sosize;
            entity.Trangthai = dto.Trangthai;

            var updatedEntity = await _repository.UpdateAsync(entity);
            return new SizeDTO
            {
                Sosize = updatedEntity.Sosize,
                Trangthai = updatedEntity.Trangthai
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

    }
}
