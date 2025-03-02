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
    public class GiamGia_RankRepo : IGiamGia_RankRepo
    {
        private readonly AppDbContext _context;

        public GiamGia_RankRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(giamgia_rank giamgia_Rank)
        {
            _context.giamgia_Ranks.Add(giamgia_Rank);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var giamgia_Rank = await GetById(id);
            if (giamgia_Rank != null)
            {
                _context.giamgia_Ranks.Remove(giamgia_Rank);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<giamgia_rank>> GetAll() => await _context.giamgia_Ranks.ToListAsync();

        public async Task<giamgia_rank> GetById(int id) => await _context.giamgia_Ranks.FindAsync(id);

        public async Task Update(giamgia_rank giamgia_Rank)
        {
            _context.giamgia_Ranks.Update(giamgia_Rank);
            await _context.SaveChangesAsync();
        }
    }
}
