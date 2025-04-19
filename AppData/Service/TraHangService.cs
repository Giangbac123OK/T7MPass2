using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using AppData.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
    public class TraHangService : ITraHangService
    {
        private readonly ITraHangRepo _repos;
        private readonly IHoaDonRepo _HDrepos;
        private readonly IKhachHangRepo _KHrepos;
        public TraHangService(ITraHangRepo repos, IHoaDonRepo hDrepos, IKhachHangRepo kHrepos)
        {
            _repos = repos;
            _HDrepos = hDrepos;
            _KHrepos = kHrepos;
        }
        public async Task Add(TrahangDTO trahang)
        {
            var a = new Trahang
            {
                Tenkhachhang = trahang.Tenkhachhang,
                Idkh = trahang.Idkh,
                Idnv = (trahang.Idnv != null && trahang.Idnv != 0) ? trahang.Idnv : (int?)null,
                Sotienhoan = trahang.Sotienhoan,
                Lydotrahang = trahang.Lydotrahang != null ? trahang.Lydotrahang : null,
                Trangthai = trahang.Trangthai,
                Phuongthuchoantien = trahang.Phuongthuchoantien,
                Chuthich = trahang.Chuthich != null ? trahang.Chuthich : null,
                Tennganhang = trahang.Tennganhang != null ? trahang.Tennganhang : null,
                Sotaikhoan = trahang.Sotaikhoan != null ? trahang.Sotaikhoan : null,
                Tentaikhoan = trahang.Tentaikhoan != null ? trahang.Tentaikhoan : null,
                Hinhthucxuly = trahang.Hinhthucxuly != null ? trahang.Hinhthucxuly : null,
                Ngaytrahangthucte = trahang.Ngaytrahangthucte != null ? trahang.Ngaytrahangthucte : null,
                Diachiship = trahang.Diachiship,
                Trangthaihoantien = trahang.Trangthaihoantien
            };
            await _repos.Add(a);

            trahang.Id = a.Id;
        }

        public async Task DeleteById(int id)
        {
            await _repos.DeleteById(id);
        }

        public async Task Doidiem(int idkh, decimal tien)
        {
            var a = await _KHrepos.GetByIdAsync(idkh);
            if (a == null)
                throw new KeyNotFoundException("Không tồn tại khách hàng.");

            a.Diemsudung += (int)Math.Round(tien, MidpointRounding.AwayFromZero);

            await _KHrepos.UpdateAsync(a);
        }

        public async Task<List<TrahangDTO>> GetAll()
        {
            var a = await _repos.GetAll();
            return a.Select(x => new TrahangDTO
            {
                Id = x.Id,
                Tenkhachhang = x.Tenkhachhang,
                Idkh = x.Idkh,
                Idnv = x.Idnv,
                Sotienhoan = x.Sotienhoan,
                Lydotrahang = x.Lydotrahang,
                Trangthai = x.Trangthai,
                Phuongthuchoantien = x.Phuongthuchoantien,
                Chuthich = x.Chuthich,
                Tennganhang = x.Tennganhang,
                Sotaikhoan = x.Sotaikhoan,
                Tentaikhoan = x.Tentaikhoan,
                Hinhthucxuly = x.Hinhthucxuly,
                Diachiship = x.Diachiship,
                Ngaytrahangthucte = x.Ngaytrahangthucte,
                Trangthaihoantien = x.Trangthaihoantien
            }).ToList();
        }
        public async Task<TrahangDTO> GetById(int id)
        {
            var x = await _repos.GetById(id);
            return new TrahangDTO
            {
                Id = x.Id,
                Tenkhachhang = x.Tenkhachhang,
                Idkh = x.Idkh,
                Idnv = x.Idnv,
                Sotienhoan = x.Sotienhoan,
                Lydotrahang = x.Lydotrahang,
                Trangthai = x.Trangthai,
                Phuongthuchoantien = x.Phuongthuchoantien,
                Chuthich = x.Chuthich,
                Tennganhang = x.Tennganhang,
                Sotaikhoan = x.Sotaikhoan,
                Tentaikhoan = x.Tentaikhoan,
                Hinhthucxuly = x.Hinhthucxuly,
                Diachiship = x.Diachiship,
                Ngaytrahangthucte = x.Ngaytrahangthucte,
                Trangthaihoantien = x.Trangthaihoantien
            };
        }

        public async Task Huydon(int id, string? chuthich)
        {
            try
            {
                var a = await _repos.GetById(id);
                if (a == null) 
                { 
                    throw new KeyNotFoundException("Mã hóa đơn không tồn tại"); 
                }
                if (a.Trangthai != 0)
                {
                    throw new KeyNotFoundException("Hóa đơn này đã được xác nhận hoặc trả hàng thành công!");
                }
                a.Chuthich = chuthich ?? null;
                a.Trangthai = 2;
                await _repos.Update(a);
            }
            catch (Exception e) 
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        public async Task Update(int id, TrahangDTO trahang)
        {
            var a = await _repos.GetById(id);
            if (a == null)
            {
                throw new KeyNotFoundException("Không tồn tại!");
            }
            else
            {
                a.Tenkhachhang = trahang.Tenkhachhang;
                a.Idkh = trahang.Idkh;
                a.Idnv = trahang.Idnv;
                a.Sotienhoan = trahang.Sotienhoan;
                a.Lydotrahang = trahang.Lydotrahang;
                a.Trangthai = trahang.Trangthai;
                a.Phuongthuchoantien = trahang.Phuongthuchoantien;
                a.Chuthich = trahang.Chuthich;
                a.Tennganhang = BCrypt.Net.BCrypt.HashPassword(trahang.Tennganhang) != null ? trahang.Tennganhang : null;
                a.Sotaikhoan = BCrypt.Net.BCrypt.HashPassword(trahang.Sotaikhoan) != null ? trahang.Sotaikhoan : null;
                a.Tentaikhoan = BCrypt.Net.BCrypt.HashPassword(trahang.Tentaikhoan) != null ? trahang.Tentaikhoan : null;
                a.Hinhthucxuly = trahang.Hinhthucxuly;
                a.Diachiship = trahang.Diachiship;
                a.Ngaytrahangthucte = trahang.Ngaytrahangthucte != null ? trahang.Ngaytrahangthucte : null;
                a.Trangthaihoantien = trahang.Trangthaihoantien;
                await _repos.Update(a);
            }
        }

        public async Task UpdateTrangThaiHd(int id)
        {
            try
            {
                var a = await _HDrepos.GetByIdAsync(id);
                if (a == null)
                {
                    throw new KeyNotFoundException("Không tồn tại!");
                }
                else
                {
                    a.Trangthai = 5;
                    await _HDrepos.UpdateAsync(a);
                }
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }
    }
}
