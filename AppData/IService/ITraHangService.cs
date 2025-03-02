using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ITraHangService
    {
        Task<List<Trahang>> GetAll();
        Task<Trahang> GetById(int id);
        Task Create(TrahangDTO dto);
        Task Update(TrahangDTO dto);
        Task Delete(int id);
    }
}
