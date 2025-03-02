using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IGiamGiaService
    {
        Task<List<Giamgia>> GetAll();
        Task<Giamgia> GetById(int id);
        Task Create(GiamgiaDTO dto);
        Task Update(GiamgiaDTO dto);
        Task Delete(int id);
    }
}
