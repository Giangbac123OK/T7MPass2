using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ISanPhamChiTietService
    {
        Task<List<Sanphamchitiet>> GetAll();
        Task<Sanphamchitiet> GetById(int id);
        Task Create(SanphamchitietDTO dto);
        Task Update(SanphamchitietDTO dto);
        Task Delete(int id);
    }
}
