﻿using AppData.DTO;
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
        private readonly IThuongHieuRepo _repository;

        public ThuongHieuService(IThuongHieuRepo repos)
        {
            _repository = repos;
        }


        public async Task<IEnumerable<ThuonghieuDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new ThuonghieuDTO
            {
                Id = e.Id,
                Tenthuonghieu = e.Tenthuonghieu,
                Tinhtrang = e.Tinhtrang
            });
        }

        public async Task<ThuonghieuDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new ThuonghieuDTO
            {
                Tenthuonghieu = entity.Tenthuonghieu,
                Tinhtrang = entity.Tinhtrang
            };
        }

        public async Task<ThuonghieuDTO> AddAsync(ThuonghieuDTO dto)
        {
            var entity = new Thuonghieu
            {
                Tenthuonghieu = dto.Tenthuonghieu,
                Tinhtrang = dto.Tinhtrang
            };

            var addedEntity = await _repository.AddAsync(entity);
            return new ThuonghieuDTO
            {
                Tenthuonghieu = addedEntity.Tenthuonghieu,
                Tinhtrang = addedEntity.Tinhtrang
            };
        }

        public async Task<ThuonghieuDTO> UpdateAsync(int id, ThuonghieuDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            entity.Tenthuonghieu = dto.Tenthuonghieu;
            entity.Tinhtrang = dto.Tinhtrang;

            var updatedEntity = await _repository.UpdateAsync(entity);
            return new ThuonghieuDTO
            {
                Tenthuonghieu = updatedEntity.Tenthuonghieu,
                Tinhtrang = updatedEntity.Tinhtrang
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
