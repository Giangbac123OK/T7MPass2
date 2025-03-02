using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IGiamGia_RankService
    {
        Task<List<giamgia_rank>> GetAll();
        Task<giamgia_rank> GetById(int id);
        Task Create(giamgia_rankDTO dto);
        Task Delete(int id);
    }
}
