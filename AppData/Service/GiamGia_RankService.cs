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
    public class GiamGia_RankService : IGiamGia_RankService
    {
        private readonly IGiamGia_RankRepo _repository;
        private readonly IGiamGiaRepo _GGrepository;
        private readonly IRankRepo _Rankrepository;
        public GiamGia_RankService(IGiamGia_RankRepo repository, IGiamGiaRepo gGrepository, IRankRepo rankrepository)
        {
            _repository = repository;
            _GGrepository = gGrepository;
            _Rankrepository = rankrepository;
        }

        public async Task Create(giamgia_rankDTO dto)
        {
            var giamgia = await _GGrepository.GetById(dto.IDgiamgia);
            if (giamgia == null) return;

            var rank = await _Rankrepository.GetById(dto.Idrank);
            if (giamgia == null) return;

            var giamgia_Rank = new giamgia_rank
            {
                IDgiamgia = dto.IDgiamgia,
                Idrank = dto.Idrank
            };

            await _repository.Create(giamgia_Rank);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<giamgia_rank>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<giamgia_rank> GetById(int id)
        {
            return await _repository.GetById(id);
        }

    }
}
