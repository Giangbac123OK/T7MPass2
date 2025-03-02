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
    public class ThuongHieuService : IThuongHieuRepo
    {
        private readonly AppDbContext _context;

        public ThuongHieuService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Thuonghieu thuonghieu)
        {
            _context.thuonghieus.Add(thuonghieu);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var thuonghieu = await GetById(id);
            if (thuonghieu != null)
            {
                _context.thuonghieus.Remove(thuonghieu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Thuonghieu>> GetAll() => await _context.thuonghieus.ToListAsync();

        public async Task<Thuonghieu> GetById(int id) => await _context.thuonghieus.FindAsync(id);

        public async Task Update(Thuonghieu thuonghieu)
        {
            _context.thuonghieus.Update(thuonghieu);
            await _context.SaveChangesAsync();
        }
    }
}
