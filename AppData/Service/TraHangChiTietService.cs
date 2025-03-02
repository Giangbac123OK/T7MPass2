using AppData.IRepository;
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
    public class TraHangChiTietService : ITraHangChiTietRepo
    {
        private readonly AppDbContext _context;

        public TraHangChiTietService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Trahangchitiet trahangchitiet)
        {
            _context.trahangchitiets.Add(trahangchitiet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var trahangchitiet = await GetById(id);
            if (trahangchitiet != null)
            {
                _context.trahangchitiets.Remove(trahangchitiet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Trahangchitiet>> GetAll() => await _context.trahangchitiets.ToListAsync();

        public async Task<Trahangchitiet> GetById(int id) => await _context.trahangchitiets.FindAsync(id);

        public async Task Update(Trahangchitiet trahangchitiet)
        {
            _context.trahangchitiets.Update(trahangchitiet);
            await _context.SaveChangesAsync();
        }
    }
}
