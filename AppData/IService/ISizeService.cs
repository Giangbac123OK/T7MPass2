using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ISizeService
    {
        Task<List<Size>> GetAll();
        Task<Size> GetById(int id);
        Task Create(SizeDTO dto);
        Task Update(SizeDTO dto);
        Task Delete(int id);
    }
}
