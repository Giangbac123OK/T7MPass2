using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IPhuongThucThanhToanService
    {
        Task<List<Phuongthucthanhtoan>> GetAll();
        Task<Phuongthucthanhtoan> GetById(int id);
        Task Create(PhuongthucthanhtoanDTO dto);
        Task Update(PhuongthucthanhtoanDTO dto);
        Task Delete(int id);
    }
}
