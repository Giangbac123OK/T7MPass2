﻿using AppData.DTO;
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
        public TraHangService(ITraHangRepo repos)
        {
            _repos = repos;
        }
        public async Task Add(TrahangDTO trahang)
        {
            var a = new Trahang
            {
                Tenkhachhang = trahang.Tenkhachhang,
                Idkh = trahang.Idkh,
                Idnv = trahang.Idnv != null ? trahang.Idnv : null,
                Sotienhoan = trahang.Sotienhoan,
                Lydotrahang = trahang.Lydotrahang != null ? trahang.Lydotrahang : null,
                Trangthai = trahang.Trangthai,
                Phuongthuchoantien = trahang.Phuongthuchoantien,
                Ngaytrahangdukien = trahang.Ngaytrahangdukien != null ? trahang.Ngaytrahangdukien : null,
                Ngaytrahangthucte = trahang.Ngaytrahangthucte != null ? trahang.Ngaytrahangthucte : null,
                Chuthich = trahang.Chuthich != null ? trahang.Chuthich : null
            };


            await _repos.Add(a);

            trahang.Id = a.Id;
        }

        public async Task DeleteById(int id)
        {
            await _repos.DeleteById(id);
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
                Ngaytrahangdukien = x.Ngaytrahangdukien,
                Ngaytrahangthucte = x.Ngaytrahangthucte,
                Chuthich = x.Chuthich
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
                Ngaytrahangdukien = x.Ngaytrahangdukien,
                Ngaytrahangthucte = x.Ngaytrahangthucte,
                Chuthich = x.Chuthich
            };
        }

        public async Task Trahangquahan()
        {
            await _repos.Trahangquahan();
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
                a.Ngaytrahangdukien = trahang.Ngaytrahangdukien;
                a.Ngaytrahangthucte = trahang.Ngaytrahangthucte;
                a.Chuthich = trahang.Chuthich;
                await _repos.Update(a);
            }
        }

        public async Task<List<TraHangViewModel>> ViewHoaDonTraByIdkh(int id)
        {
            return await _repos.ViewHoaDonTraByIdkh(id);
        }
    }
}
