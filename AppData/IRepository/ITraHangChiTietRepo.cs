using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface ITraHangChiTietRepo
    {
        Task<List<Trahangchitiet>> GetAll();
        Task<Trahangchitiet> GetById(int id);
        Task Create(Trahangchitiet trahangchitiet);
        Task Update(Trahangchitiet trahangchitiet);
        Task Delete(int id);
    }
}
