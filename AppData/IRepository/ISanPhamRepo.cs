using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface ISanPhamRepo
    {
        Task<List<Sanpham>> GetAll();
        Task<Sanpham> GetById(int id);
        Task Create(Sanpham sanpham);
        Task Update(Sanpham sanpham);
        Task Delete(int id);
    }
}
