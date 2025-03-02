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
    public class SanPhamService : ISanPhamRepo
    {

        private readonly AppDbContext _context;

        public SanPhamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Sanpham sanpham)
        {
            _context.sanphams.Add(sanpham);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var sanpham = await GetById(id);
            if (sanpham != null)
            {
                _context.sanphams.Remove(sanpham);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Sanpham>> GetAll() => await _context.sanphams.ToListAsync();

        public async Task<Sanpham> GetById(int id) => await _context.sanphams.FindAsync(id);

        public async Task Update(Sanpham sanpham)
        {
            _context.sanphams.Update(sanpham);
            await _context.SaveChangesAsync();
        }
    }
}
