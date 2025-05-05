using AppData.IRepository;
using AppData.Models;
using AppData.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.DTO;

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

        public async Task<List<TrahangDTO>> GetAll()
        {
            var trahangs = await _context.trahangs.ToListAsync();
            var trahangchitiets = await _context.trahangchitiets.ToListAsync();
            var hoadonchitiets = await _context.hoadonchitiets.ToListAsync();

            // Join và lấy Idhd từ hoadonchitiets
            var result = (from trahang in trahangs
                          join chitiet in trahangchitiets on trahang.Id equals chitiet.Idth
                          join hdct in hoadonchitiets on chitiet.Idhdct equals hdct.Id
                          group new { trahang, hdct.Idhd } by hdct.Idhd into g
                          select new TrahangDTO
                          {
                              Idhd = g.Key,
                              // Dưới đây là cách chọn dữ liệu từ trahang đầu tiên trong nhóm
                              Id = g.First().trahang.Id,
                              Tenkhachhang = g.First().trahang.Tenkhachhang,
                              Idkh = g.First().trahang.Idkh,
                              Idnv = g.First().trahang.Idnv,
                              Sotienhoan = g.Sum(x => x.trahang.Sotienhoan),
                              Lydotrahang = g.First().trahang.Lydotrahang,
                              Trangthai = g.First().trahang.Trangthai,
                              Phuongthuchoantien = g.First().trahang.Phuongthuchoantien,
                              Chuthich = g.First().trahang.Chuthich,
                              Tennganhang = g.First().trahang.Tennganhang,
                              Sotaikhoan = g.First().trahang.Sotaikhoan,
                              Tentaikhoan = g.First().trahang.Tentaikhoan,
                              Hinhthucxuly = g.First().trahang.Hinhthucxuly,
                              Diachiship = g.First().trahang.Diachiship,
                              Ngaytrahangthucte = g.First().trahang.Ngaytrahangthucte,
                              Trangthaihoantien = g.First().trahang.Trangthaihoantien,
                              Ngaytaodon = g.First().trahang.Ngaytaodon
                          }).ToList();

            return result;
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

        public async Task<TrahangDTO> GetById1(int id)
        {
            // Lấy đơn trả hàng theo id
            var trahang = await _context.trahangs.FirstOrDefaultAsync(x => x.Id == id);
            if (trahang == null)
                throw new KeyNotFoundException("Không tồn tại!");

            // Tìm các chi tiết trả hàng liên quan
            var trahangchitiets = await _context.trahangchitiets
                .Where(x => x.Idth == id)
                .ToListAsync();

            // Lấy danh sách idhdct từ chi tiết trả hàng
            var idhdcts = trahangchitiets.Select(x => x.Idhdct).Distinct().ToList();

            // Lấy hoadonchitiets để xác định idhd
            var hoadonchitiets = await _context.hoadonchitiets
                .Where(x => idhdcts.Contains(x.Id))
                .ToListAsync();

            // Lấy idhd đầu tiên từ hoadonchitiets
            var idhd = hoadonchitiets.FirstOrDefault()?.Idhd;

            if (idhd == null)
                throw new Exception("Không tìm thấy hóa đơn liên quan.");

            // Tìm tất cả các trahang liên quan cùng idhd
            var allTrahangs = await _context.trahangs.ToListAsync();
            var allTrahangchitiets = await _context.trahangchitiets.ToListAsync();
            var allHoadonchitiets = await _context.hoadonchitiets.ToListAsync();

            var relatedTrahangs = (from t in allTrahangs
                                   join ct in allTrahangchitiets on t.Id equals ct.Idth
                                   join hdct in allHoadonchitiets on ct.Idhdct equals hdct.Id
                                   where hdct.Idhd == idhd
                                   select t).Distinct().ToList();

            var result = new TrahangDTO
            {
                Idhd = idhd.Value,
                Id = relatedTrahangs.First().Id,
                Tenkhachhang = relatedTrahangs.First().Tenkhachhang,
                Idkh = relatedTrahangs.First().Idkh,
                Idnv = relatedTrahangs.First().Idnv,
                Sotienhoan = relatedTrahangs.Sum(x => x.Sotienhoan),
                Lydotrahang = relatedTrahangs.First().Lydotrahang,
                Trangthai = relatedTrahangs.First().Trangthai,
                Phuongthuchoantien = relatedTrahangs.First().Phuongthuchoantien,
                Chuthich = relatedTrahangs.First().Chuthich,
                Tennganhang = relatedTrahangs.First().Tennganhang,
                Sotaikhoan = relatedTrahangs.First().Sotaikhoan,
                Tentaikhoan = relatedTrahangs.First().Tentaikhoan,
                Hinhthucxuly = relatedTrahangs.First().Hinhthucxuly,
                Diachiship = relatedTrahangs.First().Diachiship,
                Ngaytrahangthucte = relatedTrahangs.First().Ngaytrahangthucte,
                Trangthaihoantien = relatedTrahangs.First().Trangthaihoantien,
                Ngaytaodon = relatedTrahangs.First().Ngaytaodon
            };

            return result;
        }

        public async Task Update(Trahang trhang)
        {
            _context.trahangs.Update(trhang);
            await _context.SaveChangesAsync();
        }
        private List<ViewTrahangchitiet> ViewTrahangchitiet(int id)
        {
            return (from trahangct in _context.trahangchitiets.Where(x=>x.Idth==id)
                    join hdct in _context.hoadonchitiets on trahangct.Idhdct equals hdct.Id
                    join spct in _context.Sanphamchitiets on hdct.Idspct equals spct.Id
                    join sp in _context.sanphams on spct.Idsp equals sp.Id
                    select new ViewTrahangchitiet
                    {
                        Id = trahangct.Id,
                        Idth = trahangct.Id,
                        Soluong = trahangct.Soluong,
                        Idspct = spct.Id,
                        Idsp = sp.Id
                    }).ToList();
        }
        public async Task XacNhan(int id, string hinhthucxuly, int idnv, string? chuthich)
        {
            try
            {
                var datath = await _context.trahangs.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new KeyNotFoundException("Không tồn tại mã trả hàng: " + id);

                var datathct = ViewTrahangchitiet(datath.Id);
                if (datathct == null || !datathct.Any())
                    throw new KeyNotFoundException("Không tồn tại trả hàng chi tiết");

                var kh = await _context.khachhangs.FirstOrDefaultAsync(x => x.Id == datath.Idkh)
                    ?? throw new KeyNotFoundException("Không tồn tại khách hàng");

                if (datath.Trangthaihoantien == 0)
                {
                    if (hinhthucxuly == "Trả hàng không hoàn tiền" || hinhthucxuly == "Trả hàng và hoàn tiền")
                    {
                        bool updatePoint = hinhthucxuly == "Trả hàng và hoàn tiền" && datath.Phuongthuchoantien == "Đổi điểm";

                        if (updatePoint)
                        {
                            kh.Diemsudung += (int)datath.Sotienhoan;
                            _context.khachhangs.Update(kh);
                        }
                    }

                    datath.Trangthaihoantien = 1;
                    datath.Hinhthucxuly = hinhthucxuly;
                    datath.Idnv = idnv;
                    datath.Trangthai = 1;
                    datath.Chuthich = chuthich;
                    _context.trahangs.Update(datath);

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new KeyNotFoundException("Hóa đơn đã thanh toán");
                }
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

    }
    internal class ViewTrahangchitiet
    {
        public int Id { get; set; }
        public int Idth { get; set; }
        public int Soluong { get; set; }
        public int Idspct { get; set; }
        public int Idsp {  get; set; }
    }
}
