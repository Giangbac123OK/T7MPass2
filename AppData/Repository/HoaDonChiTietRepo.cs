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
    public class HoaDonChiTetRepo : IHoaDonChiTietRepo
    {
        private readonly AppDbContext _context;

        public HoaDonChiTetRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Hoadonchitiet hoadonchitiet)
        {
            _context.hoadonchitiets.Add(hoadonchitiet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var hoadonchitiet = await GetById(id);
            if (hoadonchitiet != null)
            {
                _context.hoadonchitiets.Remove(hoadonchitiet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Hoadonchitiet>> GetAll() => await _context.hoadonchitiets.ToListAsync();

        public async Task<Hoadonchitiet> GetById(int id) => await _context.hoadonchitiets.FindAsync(id);

        public async Task Update(Hoadonchitiet hoadonchitiet)
        {
            _context.hoadonchitiets.Update(hoadonchitiet);
            await _context.SaveChangesAsync();
        }
    }
}
