using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IColorRepo
    {
        Task<List<Color>> GetAll();
        Task<Color> GetById(int id);
        Task Create(Color color);
        Task Update(Color color);
        Task Delete(int id);
    }
}
