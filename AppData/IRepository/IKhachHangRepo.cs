using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IKhachHangRepo
    {
        Task<List<Khachhang>> GetAll();
        Task<Khachhang> GetById(int id);
        Task Create(Khachhang khachhang);
        Task Update(Khachhang khachhang);
        Task Delete(int id);
    }
}
