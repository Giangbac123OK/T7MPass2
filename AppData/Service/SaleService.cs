using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
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
    public class SaleService : ISaleService
    {
        private readonly ISaleRepo _repo;

        public SaleService(ISaleRepo repo)
        {
            _repo = repo;
        }

        public async Task Create(SaleDTO sale)
        {
            var saLe = new Sale
            {
                Ten = sale.Ten,
                Mota = sale.Mota,
                Trangthai = sale.Trangthai,
                Ngaybatdau = sale.Ngaybatdau,
                Ngayketthuc = sale.Ngayketthuc,
            };
           await  _repo.Create(saLe);
        }

        public async Task Delete(int id)
        {
           await _repo.Delete(id);
        }

        public async Task<List<SaleDTO>> GetAll()
        {
            var list = await _repo.GetAll();
            return list.Select(list => new SaleDTO()
            {
                Id = list.Id,
                Ten = list.Ten,
                Trangthai = list.Trangthai,
                Mota = list.Mota,
                Ngaybatdau = list.Ngaybatdau,
                Ngayketthuc = list.Ngayketthuc,
            }).ToList();

        }

        public async Task<SaleDTO> GetById(int id)
        {
            var list = await _repo.GetById(id);
            return new SaleDTO()
            {
                Id = list.Id,
               Mota = list.Mota,
                Trangthai = list.Trangthai,
                Ngayketthuc = list.Ngayketthuc,
                Ngaybatdau = list.Ngaybatdau,
                Ten = list.Ten,
              
            };
        }

        public async Task Update(SaleDTO sale)
        {
            var itemUpdate = await _repo.GetById(sale.Id);

            itemUpdate.Ten = sale.Ten;
            itemUpdate.Mota = sale.Mota;
            itemUpdate.Ngaybatdau = sale.Ngaybatdau;
            itemUpdate.Ngayketthuc = sale.Ngayketthuc;
            itemUpdate.Trangthai = sale.Trangthai;

            await _repo.Update(itemUpdate);
        }
    }
}
