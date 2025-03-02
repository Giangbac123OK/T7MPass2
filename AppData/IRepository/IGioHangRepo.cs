using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IGioHangRepo
    {
        Task<List<Giohang>> GetAll();
        Task<Giohang> GetById(int id);
        Task Create(Giohang giohang);
        Task Update(Giohang giohang);
        Task Delete(int id);
    }
}
