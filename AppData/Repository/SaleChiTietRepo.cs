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
    public class SaleChiTietRepo : ISaleChiTietRepo
    {
        private readonly AppDbContext _context;

        public SaleChiTietRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Salechitiet salechitiet)
        {
            _context.salechitiets.Add(salechitiet);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var salechitiet = await GetById(id);
            if (salechitiet != null)
            {
                _context.salechitiets.Remove(salechitiet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Salechitiet>> GetAll() => await _context.salechitiets.ToListAsync();

        public async Task<Salechitiet> GetById(int id) => await _context.salechitiets.FindAsync(id);

        public async Task Update(Salechitiet salechitiet)
        {
            _context.salechitiets.Update(salechitiet);
            await _context.SaveChangesAsync();
        }
    }
}
