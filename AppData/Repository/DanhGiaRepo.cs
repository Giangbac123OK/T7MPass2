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
    public class DanhGiaRepo : IDanhGiaRepo
    {
        private readonly AppDbContext _context;

        public DanhGiaRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Danhgia danhgia)
        {
            _context.danhgias.Add(danhgia);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var danhgia = await GetById(id);
            if (danhgia != null)
            {
                _context.danhgias.Remove(danhgia);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Danhgia>> GetAll() => await _context.danhgias.ToListAsync();

        public async Task<Danhgia> GetById(int id) => await _context.danhgias.FindAsync(id);

        public async Task Update(Danhgia danhgia)
        {
            _context.danhgias.Update(danhgia);
            await _context.SaveChangesAsync();
        }
    }
}
