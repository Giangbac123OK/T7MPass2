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
    public class PhuongThucThanhToanService : IPhuongThucThanhToanRepo
    {
        private readonly AppDbContext _context;

        public PhuongThucThanhToanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Phuongthucthanhtoan phuongthucthanhtoan)
        {
            _context.phuongthucthanhtoans.Add(phuongthucthanhtoan);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var phuongthucthanhtoan = await GetById(id);
            if (phuongthucthanhtoan != null)
            {
                _context.phuongthucthanhtoans.Remove(phuongthucthanhtoan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Phuongthucthanhtoan>> GetAll() => await _context.phuongthucthanhtoans.ToListAsync();

        public async Task<Phuongthucthanhtoan> GetById(int id) => await _context.phuongthucthanhtoans.FindAsync(id);

        public async Task Update(Phuongthucthanhtoan phuongthucthanhtoan)
        {
            _context.phuongthucthanhtoans.Update(phuongthucthanhtoan);
            await _context.SaveChangesAsync();
        }
    }
}
