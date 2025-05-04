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

        public async Task<IEnumerable<Trahang>> GetAll()
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
