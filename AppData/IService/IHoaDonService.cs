using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IHoaDonService
    {
        Task<List<Hoadon>> GetAll();
        Task<Hoadon> GetById(int id);
        Task Create(HoadonDTO dto);
        Task Update(HoadonDTO dto);
        Task Delete(int id);
    }
}
