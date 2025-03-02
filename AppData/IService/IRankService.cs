using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IRankService
    {
        Task<List<Rank>> GetAll();
        Task<Rank> GetById(int id);
        Task Create(RankDTO dto);
        Task Update(RankDTO dto);
        Task Delete(int id);
    }
}
