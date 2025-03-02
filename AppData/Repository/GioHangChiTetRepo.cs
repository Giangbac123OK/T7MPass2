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
    public class GioHangChiTetRepo : IGioHangChiTetRepo
    {
        private readonly AppDbContext _context;

        public GioHangChiTetRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Giohangchitiet giohangchitiet)
        {
            _context.giohangchitiets.Add(giohangchitiet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var giohangchitiet = await GetById(id);
            if (giohangchitiet != null)
            {
                _context.giohangchitiets.Remove(giohangchitiet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Giohangchitiet>> GetAll() => await _context.giohangchitiets.ToListAsync();

        public async Task<Giohangchitiet> GetById(int id) => await _context.giohangchitiets.FindAsync(id);

        public async Task Update(Giohangchitiet giohangchitiet)
        {
            _context.giohangchitiets.Update(giohangchitiet);
            await _context.SaveChangesAsync();
        }
    }
}
