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
    public class GioHangService : IGioHangRepo
    {
        private readonly AppDbContext _context;

        public GioHangService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Giohang giohang)
        {
            _context.giohangs.Add(giohang);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var giohang = await GetById(id);
            if (giohang != null)
            {
                _context.giohangs.Remove(giohang);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Giohang>> GetAll() => await _context.giohangs.ToListAsync();

        public async Task<Giohang> GetById(int id) => await _context.giohangs.FindAsync(id);

        public async Task Update(Giohang giohang)
        {
            _context.giohangs.Update(giohang);
            await _context.SaveChangesAsync();
        }
    }
}
