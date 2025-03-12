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
    public interface ITraHangChiTietService
    {
        Task<List<TrahangchitietDTO>> GetAll();
        Task<TrahangchitietDTO> GetById(int id);
        Task<List<TrahangchitietDTO>> GetByMaHD(int id);
        Task Add(TrahangchitietDTO ct);
        Task UpdateSoluongTra(int idhdct, int soluong);
        Task Update(int id, TrahangchitietDTO ct);
        Task Delete(int id);
        Task<List<TrahangchitietViewModel>> ViewHoadonctTheoIdth(int id);
    }
}
