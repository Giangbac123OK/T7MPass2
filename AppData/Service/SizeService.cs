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
    public class SizeService : ISizeRepo
    {
        private readonly AppDbContext _context;

        public SizeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Models.Size size)
        {
            _context.sizes.Add(size);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var size = await GetById(id);
            if (size != null)
            {
                _context.sizes.Remove(size);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Models.Size>> GetAll() => await _context.sizes.ToListAsync();

        public async Task<Models.Size> GetById(int id) => await _context.sizes.FindAsync(id);

        public async Task Update(Models.Size size)
        {
            _context.sizes.Update(size);
            await _context.SaveChangesAsync();
        }
    }
}
