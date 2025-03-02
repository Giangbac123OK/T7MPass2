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
    public class SaleService : ISaleRepo
    {
        private readonly AppDbContext _context;

        public SaleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Sale sale)
        {
            _context.sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var sale = await GetById(id);
            if (sale != null)
            {
                _context.sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Sale>> GetAll() => await _context.sales.ToListAsync();

        public async Task<Sale> GetById(int id) => await _context.sales.FindAsync(id);

        public async Task Update(Sale sale)
        {
            _context.sales.Update(sale);
            await _context.SaveChangesAsync();
        }
    }
}
