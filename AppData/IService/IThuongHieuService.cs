using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IThuongHieuService
    {
        Task<List<Thuonghieu>> GetAll();
        Task<Thuonghieu> GetById(int id);
        Task Create(ThuonghieuDTO dto);
        Task Update(ThuonghieuDTO dto);
        Task Delete(int id);
    }
}
