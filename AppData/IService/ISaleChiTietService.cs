using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ISaleChiTietService
    {
        Task<List<Salechitiet>> GetAll();
        Task<Salechitiet> GetById(int id);
        Task Create(SalechitietDTO dto);
        Task Update(SalechitietDTO dto);
        Task Delete(int id);
    }
}
