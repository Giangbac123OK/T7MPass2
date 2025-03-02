using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IPhuongThucThanhToanRepo
    {
        Task<List<Phuongthucthanhtoan>> GetAll();
        Task<Phuongthucthanhtoan> GetById(int id);
        Task Create(Phuongthucthanhtoan phuongthucthanhtoan);
        Task Update(Phuongthucthanhtoan phuongthucthanhtoan);
        Task Delete(int id);
    }
}
