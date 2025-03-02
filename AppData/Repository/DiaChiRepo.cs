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
    public class DiaChiRepo : IDiaChiRepo
    {
        private readonly AppDbContext _context;

        public DiaChiRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Diachi diachi)
        {
            _context.diachis.Add(diachi);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var diachi = await GetById(id);
            if (diachi != null)
            {
                _context.diachis.Remove(diachi);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Diachi>> GetAll() => await _context.diachis.ToListAsync();

        public async Task<Diachi> GetById(int id) => await _context.diachis.FindAsync(id);

        public async Task Update(Diachi diachi)
        {
            _context.diachis.Update(diachi);
            await _context.SaveChangesAsync();
        }
    }
}
