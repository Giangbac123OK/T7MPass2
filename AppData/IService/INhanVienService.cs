using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface INhanVienService
    {
        Task<List<Nhanvien>> GetAll();
        Task<Nhanvien> GetById(int id);
        Task Create(NhanvienDTO dto);
        Task Update(NhanvienDTO dto);
        Task Delete(int id);
    }
}
