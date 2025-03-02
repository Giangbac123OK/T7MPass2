using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IDiaChiService
    {
        Task<List<Diachi>> GetAll();
        Task<Diachi> GetById(int id);
        Task Create(DiachiDTO dto);
        Task Update(DiachiDTO dto);
        Task Delete(int id);
    }
}
