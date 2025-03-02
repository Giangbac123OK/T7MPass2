using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ITraHangChiTietService
    {
        Task<List<Trahangchitiet>> GetAll();
        Task<Trahangchitiet> GetById(int id);
        Task Create(TrahangchitietDTO dto);
        Task Update(TrahangchitietDTO dto);
        Task Delete(int id);
    }
}
