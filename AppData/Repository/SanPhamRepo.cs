using AppData.IRepository;
using AppData.Migrations;
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
    public class SanPhamRepo : ISanPhamRepo
    {

        private readonly AppDbContext _context;

        public SanPhamRepo(AppDbContext context)
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

        public async Task<List<Sanpham>> SpNoiBat()
        {
            var topProducts = await (from hdct in _context.hoadonchitiets
                               join spct in _context.Sanphamchitiets on hdct.Idspct equals spct.Id
                               join s in _context.sanphams on spct.Idsp equals s.Id
                               where s.Trangthai == 0
                               group new { hdct, s } by new
                               {
                                   s.Id,
                                   s.TenSanpham,
                                   s.Trangthai,
                                   s.Soluong,
                                   s.GiaBan,
                                   s.NgayThemMoi,
                                   s.Chieudai,
                                   s.Chieurong,
                                   s.Trongluong,
                                   s.Idth,
                                   s.Mota
                               } into g
                               orderby g.Sum(x => x.hdct.Soluong) descending
                               select new Sanpham()
                               {
                                   Id = g.Key.Id,
                                   TenSanpham = g.Key.TenSanpham,
                                   Trangthai = g.Key.Trangthai,
                                   Soluong = g.Key.Soluong,
                                   GiaBan = g.Key.GiaBan,
                                   Idth = g.Key.Idth,
                                   Mota = g.Key.Mota,
                                   NgayThemMoi = g.Key.NgayThemMoi
                               }).Take(4).ToListAsync();
            return topProducts;
        }
        public async Task<List<Sanpham>> SpMoiNhat()
        {
            var topProducts = await _context.sanphams.OrderByDescending(x=>x.NgayThemMoi).Take(4).ToListAsync();
            return topProducts;
        }
        public async Task Update(Sanpham sanpham)
        {
            _context.sanphams.Update(sanpham);
            await _context.SaveChangesAsync();
        }
    }
}
