using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IThuongHieuRepo
    {
        Task<List<Thuonghieu>> GetAll();
        Task<Thuonghieu> GetById(int id);
        Task Create(Thuonghieu thuonghieu);
        Task Update(Thuonghieu thuonghieu);
        Task Delete(int id);
    }
}
