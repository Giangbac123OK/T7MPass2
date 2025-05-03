using AppData.Models;
using AppData.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface ITraHangRepo
    {
        Task<IEnumerable<Trahang>> GetAll();
        Task<Trahang> GetById(int id);
        Task Add(Trahang trhang);
        Task Update(Trahang trhang);
        Task DeleteById(int id); 
    }
}
