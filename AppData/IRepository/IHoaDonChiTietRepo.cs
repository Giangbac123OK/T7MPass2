using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IHoaDonChiTietRepo
    {
        Task<List<Hoadonchitiet>> GetAll();
        Task<Hoadonchitiet> GetById(int id);
        Task Create(Hoadonchitiet hoadonchitiet);
        Task Update(Hoadonchitiet hoadonchitiet);
        Task Delete(int id);
    }
}
