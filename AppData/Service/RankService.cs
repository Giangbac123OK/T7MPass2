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
    public class RankService : IRankService
    {
        private readonly IRankRepo _repository;
        public RankService(IRankRepo repository)
        {
            _repository = repository;

        }

        public async Task Create(RankDTO dto)
        {
            var rank = new Rank
            {
                Tenrank = dto.Tenrank,
                MinMoney = dto.MinMoney,
                MaxMoney = dto.MaxMoney,
                Trangthai = dto.Trangthai
            };

            await _repository.Create(rank);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Rank>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Rank> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(RankDTO dto)
        {
            var rank = await _repository.GetById(dto.Id);
            if (rank == null) return;

            rank.Tenrank = dto.Tenrank;
            rank.MinMoney = dto.MinMoney;
            rank.MaxMoney = dto.MaxMoney;
            rank.Trangthai = dto.Trangthai;

            await _repository.Update(rank);
        }
    }
}
