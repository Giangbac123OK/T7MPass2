using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IHoaDonChiTietService
    {
        Task<List<Hoadonchitiet>> GetAll();
        Task<Hoadonchitiet> GetById(int id);
        Task Create(HoadonchitietDTO dto);
        Task Update(HoadonchitietDTO dto);
        Task Delete(int id);
    }
}
