using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IGiamGiaRepo
    {
        Task<List<giamgia_rank>> GetAll();
        Task<giamgia_rank> GetById(int id);
        Task Create(giamgia_rank giamgia_Rank);
        Task Update(giamgia_rank giamgia_Rank);
        Task Delete(int id);
    }
}
