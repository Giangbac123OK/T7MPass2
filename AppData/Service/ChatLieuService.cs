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
    public class ChatLieuService : IChatLieuService
    {
        private readonly IChatLieuRepo _repository;

        public ChatLieuService(IChatLieuRepo repos)
        {
            _repository = repos;
        }


        public async Task<IEnumerable<ChatLieuDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new ChatLieuDTO
            {
                Id = e.Id,
                Tenchatlieu = e.Tenchatlieu,
                Trangthai = e.Trangthai
            });
        }

        public async Task<ChatLieuDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new ChatLieuDTO
            {   
                Id = entity.Id,
                Tenchatlieu = entity.Tenchatlieu,
                Trangthai = entity.Trangthai
            };
        }

        public async Task<ChatLieuDTO> AddAsync(ChatLieuDTO dto)
        {
            var entity = new Models.ChatLieu
            {   
                Tenchatlieu = dto.Tenchatlieu,
                Trangthai = dto.Trangthai
            };

            var addedEntity = await _repository.AddAsync(entity);
            return new ChatLieuDTO
            {   
                Tenchatlieu = addedEntity.Tenchatlieu,
                Trangthai = addedEntity.Trangthai
            };
        }

        public async Task<ChatLieuDTO> UpdateAsync(int id, ChatLieuDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            entity.Tenchatlieu = dto.Tenchatlieu;
            entity.Trangthai = dto.Trangthai;

            var updatedEntity = await _repository.UpdateAsync(entity);
            return new ChatLieuDTO
            { 
                Tenchatlieu = updatedEntity.Tenchatlieu,
                Trangthai = updatedEntity.Trangthai
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
