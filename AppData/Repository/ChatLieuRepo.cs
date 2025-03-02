using AppData.IRepository;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public class ChatLieuRepo : IChatLieuRepo
    {
        private readonly AppDbContext _context;

        public ChatLieuRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(ChatLieu chatLieu)
        {
            _context.chatLieus.Add(chatLieu);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var chatLieu = await GetById(id);
            if (chatLieu != null)
            {
                _context.chatLieus.Remove(chatLieu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ChatLieu>> GetAll() => await _context.chatLieus.ToListAsync();

        public async Task<ChatLieu> GetById(int id) => await _context.chatLieus.FindAsync(id);


        public async Task Update(ChatLieu chatLieu)
        {
            _context.chatLieus.Update(chatLieu);
            await _context.SaveChangesAsync();
        }
    }
}
