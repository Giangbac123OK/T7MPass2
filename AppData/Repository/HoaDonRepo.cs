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
    public class HoaDonRepo : IHoaDonRepo
    {
        private readonly AppDbContext _context;

        public HoaDonRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Hoadon hoadon)
        {
            _context.hoadons.Add(hoadon);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var hoadon = await GetById(id);
            if (hoadon != null)
            {
                _context.hoadons.Remove(hoadon);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Hoadon>> GetAll() => await _context.hoadons.ToListAsync();

        public async Task<Hoadon> GetById(int id) => await _context.hoadons.FindAsync(id);

        public async Task Update(Hoadon hoadon)
        {
            _context.hoadons.Update(hoadon);
            await _context.SaveChangesAsync();
        }
    }
}
