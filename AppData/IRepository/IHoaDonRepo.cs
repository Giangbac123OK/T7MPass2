using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IHoaDonRepo
    {
        Task<List<Hoadon>> GetAll();
        Task<Hoadon> GetById(int id);
        Task Create(Hoadon hoadon);
        Task Update(Hoadon hoadon);
        Task Delete(int id);
    }
}
