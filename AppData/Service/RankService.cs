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
    public class RankService : IRankRepo
    {
        private readonly AppDbContext _context;

        public RankService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Rank rank)
        {
            _context.ranks.Add(rank);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var rank = await GetById(id);
            if (rank != null)
            {
                _context.ranks.Remove(rank);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Rank>> GetAll() => await _context.ranks.ToListAsync();

        public async Task<Rank> GetById(int id) => await _context.ranks.FindAsync(id);

        public async Task Update(Rank rank)
        {
            _context.ranks.Update(rank);
            await _context.SaveChangesAsync();
        }
    }
}
