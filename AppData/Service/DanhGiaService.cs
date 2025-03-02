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
    public class DanhGiaService : IDanhGiaService
    {
        private readonly IDanhGiaRepo _repository;
        private readonly IKhachHangRepo _KHrepository;
        public DanhGiaService(IDanhGiaRepo repository, IKhachHangRepo kHrepository)
        {
            _repository = repository;
            _KHrepository = kHrepository;
        }

        public async Task Create(DanhgiaDTO dto)
        {
            var khachhang = await _KHrepository.GetById(dto.Idkh);
            if (khachhang == null) return;

            var danhgia = new Danhgia
            {
                Idkh = dto.Idkh,
                Noidungdanhgia = dto.Noidungdanhgia,
                Ngaydanhgia = dto.Ngaydanhgia,
                Idhdct = dto.Idhdct,
                Sosao = dto.Sosao
            };

            await _repository.Create(danhgia);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Danhgia>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Danhgia> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(DanhgiaDTO dto)
        {
            var khachhang = await _KHrepository.GetById(dto.Idkh);
            if (khachhang == null) return;

            var danhgia = await _repository.GetById(dto.Id);
            if (danhgia == null) return;

            danhgia.Idkh = dto.Idkh;
            danhgia.Noidungdanhgia = dto.Noidungdanhgia;
            danhgia.Ngaydanhgia = dto.Ngaydanhgia;
            danhgia.Idhdct = dto.Idhdct;
            danhgia.Sosao = dto.Sosao;

            await _repository.Update(danhgia);
        }
    }
}
