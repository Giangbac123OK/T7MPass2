using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface ITraHangRepo
    {
        Task<List<Trahang>> GetAll();
        Task<Trahang> GetById(int id);
        Task Create(Trahang trahang);
        Task Update(Trahang trahang);
        Task Delete(int id);
    }
}
