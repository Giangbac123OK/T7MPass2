using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IGioHangChiTetRepo
    {
        Task<List<Giohangchitiet>> GetAll();
        Task<Giohangchitiet> GetById(int id);
        Task Create(Giohangchitiet giohangchitiet);
        Task Update(Giohangchitiet giohangchitiet);
        Task Delete(int id);
    }
}
