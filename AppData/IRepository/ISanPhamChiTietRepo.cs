using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface ISanPhamChiTietRepo
    {
        Task<List<Sanphamchitiet>> GetAll();
        Task<Sanphamchitiet> GetById(int id);
        Task Create(Sanphamchitiet sanphamchitiet);
        Task Update(Sanphamchitiet sanphamchitiet);
        Task Delete(int id);
    }
}
