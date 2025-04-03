using AppData.IRepository;
using AppData.Models;
using AppData.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public class TraHangRepo : ITraHangRepo
    {
        private readonly AppDbContext _context;
        public TraHangRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Trahang trhang)
        {
            try
            {
                await _context.trahangs.AddAsync(trhang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Lỗi khi thêm trả hàng vào cơ sở dữ liệu.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi không xác định khi thêm trả hàng.", ex);
            }
        }

        public async Task DeleteById(int id)
        {
            var a = await _context.trahangs.FirstOrDefaultAsync(x => x.Id == id);
            if (a != null)
            {
                _context.trahangs.Remove(a);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Không tồn tại!");
            }
        }

        public async Task<List<Trahang>> GetAll()
        {
            return await _context.trahangs.ToListAsync();
        }

        public async Task<Trahang> GetById(int id)
        {
            var a = await _context.trahangs.FirstOrDefaultAsync(x => x.Id == id);
            if (a != null)
            {
                return a;
            }
            else
            {
                throw new KeyNotFoundException("Không tồn tại!");
            }
        }

        public async Task Update(Trahang trhang)
        {
            _context.trahangs.Update(trhang);
            await _context.SaveChangesAsync();
        }
    }
}
