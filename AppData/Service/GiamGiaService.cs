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
using AutoMapper;

namespace AppData.Service
{
    public class GiamGiaService : IGiamGiaService
    {
        private readonly IGiamGiaRepo _repository;
        private readonly IMapper _mapper;
        public GiamGiaService(IGiamGiaRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<GiamgiaDTO>> GetAllAsync()
        {
            var giamgias = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GiamgiaDTO>>(giamgias);
        }

        public async Task<GiamgiaDTO> GetByIdAsync(int id)
        {
            var giamgia = await _repository.GetByIdAsync(id);
            if (giamgia == null) throw new KeyNotFoundException("Không tìm thấy mã giảm giá.");

            return _mapper.Map<GiamgiaDTO>(giamgia);
        }

        public async Task AddAsync(GiamgiaDTO dto)
        {
            var giamgia = _mapper.Map<Giamgia>(dto);
            await _repository.AddAsync(giamgia);

        }

        public async Task UpdateAsync(int id, GiamgiaDTOupdate dto)
        {
            var existingGiamgia = await _repository.GetByIdAsync(id);
            if (existingGiamgia == null) throw new KeyNotFoundException("Không tìm thấy mã giảm giá.");

            existingGiamgia.Mota = dto.Mota;
            existingGiamgia.Donvi = dto.Donvi;
            existingGiamgia.Giatri = dto.Giatri;
            existingGiamgia.Ngaybatdau = dto.Ngaybatdau;
            existingGiamgia.Soluong = dto.Soluong;
            existingGiamgia.Ngayketthuc = dto.Ngayketthuc;
            existingGiamgia.Trangthai = dto.Trangthai;
            await _repository.UpdateAsync(existingGiamgia);

        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task AddRankToGiamgia(GiamgiaDTO dto)
        {
            var giamgia = new Giamgia
            {
                Mota = dto.Mota,
                Donvi = dto.Donvi,
                Giatri = dto.Giatri,
                Ngaybatdau = dto.Ngaybatdau,
                Soluong = dto.Soluong,
                Ngayketthuc = dto.Ngayketthuc,
                Trangthai = dto.Trangthai
            };

            await _repository.AddAsync(giamgia);
            await _repository.AddRankToGiamgia(giamgia.Id, dto.RankNames);
        }
    }
}
