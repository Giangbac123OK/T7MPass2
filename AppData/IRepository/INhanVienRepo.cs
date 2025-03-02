using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface INhanVienRepo
    {
        Task<List<Nhanvien>> GetAll();
        Task<Nhanvien> GetById(int id);
        Task Create(Nhanvien nhanvien);
        Task Update(Nhanvien nhanvien);
        Task Delete(int id);
    }
}
