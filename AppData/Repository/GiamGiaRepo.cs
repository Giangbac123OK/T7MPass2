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
    public class GiamGiaRepo : IGiamGiaRepo
    {
        private readonly AppDbContext _context;

        public GiamGiaRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Giamgia giamgia)
        {
            _context.giamgias.Add(giamgia);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var giamgia = await GetById(id);
            if (giamgia != null)
            {
                _context.giamgias.Remove(giamgia);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Giamgia>> GetAll() => await _context.giamgias.ToListAsync();

        public async Task<Giamgia> GetById(int id) => await _context.giamgias.FindAsync(id);

        public async Task Update(Giamgia giamgias)
        {
            _context.giamgias.Update(giamgias);
            await _context.SaveChangesAsync();
        }
    }
}
