using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IDanhGiaService
    {
        Task<List<Danhgia>> GetAll();
        Task<Danhgia> GetById(int id);
        Task Create(DanhgiaDTO dto);
        Task Update(DanhgiaDTO dto);
        Task Delete(int id);
    }
}
