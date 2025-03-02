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
    public class HinhAnhRepo : IHinhAnhRepo
    {
        private readonly AppDbContext _context;

        public HinhAnhRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Hinhanh hinhanh)
        {
            _context.hinhanhs.Add(hinhanh);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var hinhanh = await GetById(id);
            if (hinhanh != null)
            {
                _context.hinhanhs.Remove(hinhanh);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Hinhanh>> GetAll() => await _context.hinhanhs.ToListAsync();

        public async Task<Hinhanh> GetById(int id) => await _context.hinhanhs.FindAsync(id);

        public async Task Update(Hinhanh hinhanh)
        {
            _context.hinhanhs.Update(hinhanh);
            await _context.SaveChangesAsync();
        }
    }
}
