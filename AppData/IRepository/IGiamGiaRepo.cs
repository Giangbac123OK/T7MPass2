using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IGiamGia
    {
        Task<List<Giamgia>> GetAll();
        Task<Giamgia> GetById(int id);
        Task Create(Giamgia giamgia);
        Task Update(Giamgia giamgia);
        Task Delete(int id);
    }
}
