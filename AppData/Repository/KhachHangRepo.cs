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
    public class KhachHangRepo : IKhachHangRepo
    {
        private readonly AppDbContext _context;

        public KhachHangRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Khachhang khachhang)
        {
            _context.khachhangs.Add(khachhang);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var khachhang = await GetById(id);
            if (khachhang != null)
            {
                _context.khachhangs.Remove(khachhang);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Khachhang>> GetAll() => await _context.khachhangs.ToListAsync();

        public async Task<Khachhang> GetById(int id) => await _context.khachhangs.FindAsync(id);

        public async Task Update(Khachhang khachhang)
        {
            _context.khachhangs.Update(khachhang);
            await _context.SaveChangesAsync();
        }
    }
}
