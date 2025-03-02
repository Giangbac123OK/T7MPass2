using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IRankRepo
    {
        Task<List<Rank>> GetAll();
        Task<Rank> GetById(int id);
        Task Create(Rank rank);
        Task Update(Rank rank);
        Task Delete(int id);
    }
}
