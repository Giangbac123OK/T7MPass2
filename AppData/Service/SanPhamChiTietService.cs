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
    public class SanPhamChiTietService : ISanPhamChiTietRepo
    {
        private readonly AppDbContext _context;

        public SanPhamChiTietService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Sanphamchitiet sanphamchitiet)
        {
            _context.Sanphamchitiets.Add(sanphamchitiet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var sanphamchitiet = await GetById(id);
            if (sanphamchitiet != null)
            {
                _context.Sanphamchitiets.Remove(sanphamchitiet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Sanphamchitiet>> GetAll() => await _context.Sanphamchitiets.ToListAsync();

        public async Task<Sanphamchitiet> GetById(int id) => await _context.Sanphamchitiets.FindAsync(id);

        public async Task Update(Sanphamchitiet sanphamchitiet)
        {
            _context.Sanphamchitiets.Update(sanphamchitiet);
            await _context.SaveChangesAsync();
        }
    }
}
