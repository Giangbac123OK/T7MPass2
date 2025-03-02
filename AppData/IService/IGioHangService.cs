using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IGioHangService
    {
        Task<List<Giohang>> GetAll();
        Task<Giohang> GetById(int id);
        Task Create(GiohangDTO dto);
        Task Update(GiohangDTO dto);
        Task Delete(int id);
    }
}
