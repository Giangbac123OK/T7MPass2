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
    public class TraHangRepo : ITraHangRepo
    {
        private readonly AppDbContext _context;

        public TraHangRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Trahang trahang)
        {
            _context.trahangs.Add(trahang);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var trahang = await GetById(id);
            if (trahang != null)
            {
                _context.trahangs.Remove(trahang);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Trahang>> GetAll() => await _context.trahangs.ToListAsync();

        public async Task<Trahang> GetById(int id) => await _context.trahangs.FindAsync(id);

        public async Task Update(Trahang trahang)
        {
            _context.trahangs.Update(trahang);
            await _context.SaveChangesAsync();
        }
    }
}
