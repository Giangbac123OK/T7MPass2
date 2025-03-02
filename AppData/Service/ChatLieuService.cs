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
        public ChatLieuService(IChatLieuRepo repository)
        {
            _repository = repository;

        }

        public async Task Create(ChatLieuDTO dto)
        {
            var color = new ChatLieu
            {
                Tenchatlieu = dto.Tenchatlieu,
                Trangthai = dto.Trangthai
            };

            await _repository.Create(color);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<ChatLieu>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ChatLieu> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(ChatLieuDTO dto)
        {
            var chatlieu = await _repository.GetById(dto.Id);
            if (chatlieu == null) return;

            chatlieu.Tenchatlieu = dto.Tenchatlieu;
            chatlieu.Trangthai = dto.Trangthai;

            await _repository.Update(chatlieu);
        }
    }
}
