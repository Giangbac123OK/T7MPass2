using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface ISizeRepo
    {
        Task<List<Size>> GetAll();
        Task<Size> GetById(int id);
        Task Create(Size size);
        Task Update(Size size);
        Task Delete(int id);
    }
}
