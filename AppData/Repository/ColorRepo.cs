using AppData.IRepository;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public class ColorRepo : IColorRepo
    {
        private readonly AppDbContext _context;

        public ColorRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Color color)
        {
            _context.colors.Add(color);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var color = await GetById(id);
            if (color != null)
            {
                _context.colors.Remove(color);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Color>> GetAll() => await _context.colors.ToListAsync();

        public async Task<Color> GetById(int id) => await _context.colors.FindAsync(id);

        public async Task Update(Color color)
        {
            _context.colors.Update(color);
            await _context.SaveChangesAsync();
        }
    }
}
