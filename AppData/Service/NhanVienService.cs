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
    public class NhanVienService : INhanVienRepo
    {
        private readonly AppDbContext _context;

        public NhanVienService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Nhanvien nhanvien)
        {
            _context.nhanviens.Add(nhanvien);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var nhanvien = await GetById(id);
            if (nhanvien != null)
            {
                _context.nhanviens.Remove(nhanvien);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Nhanvien>> GetAll() => await _context.nhanviens.ToListAsync();

        public async Task<Nhanvien> GetById(int id) => await _context.nhanviens.FindAsync(id);

        public async Task Update(Nhanvien nhanvien)
        {
            _context.nhanviens.Update(nhanvien);
            await _context.SaveChangesAsync();
        }
    }
}
