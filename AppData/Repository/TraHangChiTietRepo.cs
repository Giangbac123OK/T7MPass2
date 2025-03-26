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
    public class TraHangChiTietRepo : ITraHangChiTietRepo
    {
        private readonly AppDbContext _context;
        public TraHangChiTietRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Trahangchitiet ct)
        {
            await _context.trahangchitiets.AddAsync(ct);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var a = await _context.trahangchitiets.FirstOrDefaultAsync(x => x.Id == id);
            if (a != null)
            {
                _context.trahangchitiets.Remove(a);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Không tồn tại!");
            }
        }

        public async Task<List<Trahangchitiet>> GetAll()
        {
            return await _context.trahangchitiets.ToListAsync();
        }

        public async Task<Trahangchitiet> GetById(int id)
        {
            return await _context.trahangchitiets.FindAsync(id);
        }

        public async Task<List<HoadonchitietViewModel>> ListSanPhamByIdhd(int id)
        {
            var data = await _context.hoadonchitiets
                .Where(hdct => hdct.Idhd == id)
                .Join(_context.Sanphamchitiets, hdct => hdct.Idspct, spct => spct.Id, (hdct, spct) => new { hdct, spct })
                .Join(_context.sanphams, x => x.spct.Idsp, sp => sp.Id, (x, sp) => new { x.hdct, x.spct, sp })
                .Join(_context.hoadons, x => x.hdct.Idhd, hd => hd.Id, (x, hd) => new { x.hdct, x.spct, x.sp, hd })
                .Join(_context.colors, x => x.spct.IdMau, mau => mau.Id, (x, mau) => new { x.hdct, x.spct, x.sp, x.hd, mau })
                .Join(_context.sizes, x => x.spct.IdSize, size => size.Id, (x, size) => new { x.hdct, x.spct, x.sp, x.hd, x.mau, size })
                .Join(_context.thuonghieus,x=>x.sp.Idth, th => th.Id, (x, th) => new { x.hdct, x.spct, x.sp, x.hd, x.mau, x.size, th })
                .Join(_context.chatLieus, x => x.spct.IdChatLieu, chatlieu => chatlieu.Id, (x, chatlieu) => new HoadonchitietViewModel
                {
                    Id = x.hdct.Id,
                    Idhd = id,
                    Idsp = x.sp.Id,
                    Idspct = x.spct.Id,
                    Tensp = $"[{x.th.Tenthuonghieu}] -  {x.sp.TenSanpham}",
                    urlHinhanh = x.spct.UrlHinhanh,
                    Giasp = x.hdct.Giasp,
                    Giamgia = x.hdct.Giamgia,
                    Soluong = x.hdct.Soluong,
                    Trangthaihd = x.hd.Trangthai,
                    Mau = x.mau.Tenmau,
                    Size = x.size.Sosize,
                    Chatlieu = chatlieu.Tenchatlieu
                })
                .AsNoTracking() // Giúp cải thiện hiệu suất
                .ToListAsync();

            return data ?? new List<HoadonchitietViewModel>(); // Tránh trả về null
        }

        public async Task Update(Trahangchitiet ct)
        {
            _context.trahangchitiets.Update(ct);
            await _context.SaveChangesAsync();
        }
    }
}
