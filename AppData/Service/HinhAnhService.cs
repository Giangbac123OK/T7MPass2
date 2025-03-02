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
    public class HinhAnhService : IHinhAnhService
    {
        private readonly IHinhAnhRepo _repository;
        private readonly ITraHangRepo _THrepository;
        private readonly IDanhGiaRepo _DGrepository;
        public HinhAnhService(IHinhAnhRepo repository, ITraHangRepo tHrepository, IDanhGiaRepo dGrepository)
        {
            _repository = repository;
            _THrepository = tHrepository;
            _DGrepository = dGrepository;
        }

        public async Task Create(HinhanhDTO dto)
        {
            if(dto.Iddanhgia != null)
            {
                int iddanhgia = (int)dto.Iddanhgia;
                var danhgia = await _DGrepository.GetById(iddanhgia);
                if (danhgia == null) return;
            }

            if (dto.Idtrahang != null)
            {
                int idtrahang = (int)dto.Idtrahang;
                var trahang = await _THrepository.GetById(idtrahang);
                if (trahang == null) return;
            }

            var hinhanh = new Hinhanh
            {
                Urlhinhanh = dto.Urlhinhanh,
                Idtrahang = dto.Idtrahang,
                Iddanhgia = dto.Iddanhgia
            };

            await _repository.Create(hinhanh);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Hinhanh>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Hinhanh> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(HinhanhDTO dto)
        {
            var hinhanh = await _repository.GetById(dto.Id);
            if (hinhanh == null) return;

            if (dto.Iddanhgia != null)
            {
                int iddanhgia = (int)dto.Iddanhgia;
                var danhgia = await _DGrepository.GetById(iddanhgia);
                if (danhgia == null) return;
            }

            if (dto.Idtrahang != null)
            {
                int idtrahang = (int)dto.Idtrahang;
                var trahang = await _THrepository.GetById(idtrahang);
                if (trahang == null) return;
            }

            hinhanh.Urlhinhanh = dto.Urlhinhanh;
            hinhanh.Idtrahang = dto.Idtrahang;
            hinhanh.Iddanhgia = dto.Iddanhgia;

            await _repository.Update(hinhanh);
        }
    }
}
