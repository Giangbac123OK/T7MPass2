using AppData.DTO;
using AppData.Models;
using AppData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IKhachHangService
    {
        Task<List<Khachhang>> GetAll();
        Task<Khachhang> GetById(int id);
        Task Create(KhachhangDTO dto);
        Task Update(KhachhangDTO dto);
        Task Delete(int id);
    }
}
