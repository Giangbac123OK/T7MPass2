using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IColorService
    {
        Task<List<Color>> GetAll();
        Task<Color> GetById(int id);
        Task Create(ColorDTO dto);
        Task Update(ColorDTO dto);
        Task Delete(int id);
    }
}
