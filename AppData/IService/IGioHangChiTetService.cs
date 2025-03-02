using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IGioHangChiTetService
    {
        Task<List<Giohangchitiet>> GetAll();
        Task<Giohangchitiet> GetById(int id);
        Task Create(GiohangchitietDTO dto);
        Task Update(GiohangchitietDTO dto);
        Task Delete(int id);
    }
}
