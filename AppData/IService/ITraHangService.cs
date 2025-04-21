using AppData.DTO;
using AppData.Models;
using AppData.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ITraHangService
    {
        Task<List<TrahangDTO>> GetAll();
        Task<TrahangDTO> GetById(int id);
        Task Add(TrahangDTO trahang);
        Task Update(int id, TrahangDTO trahang);
        Task DeleteById(int id);
        Task UpdateTrangThaiHd(int id);
        Task Xacnhan(int idth, string hinhthuc, decimal tien, string? ghichu);
        Task Huydon(int id, string? chuthich);
    }
}
