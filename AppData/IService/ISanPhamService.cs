using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ISanPhamService
    {
        Task<List<Sanpham>> GetAll();
        Task<Sanpham> GetById(int id);
        Task Create(SanphamDTO dto);
        Task Update(SanphamDTO dto);
        Task Delete(int id); 
        Task<List<SanphamDTO>> SpNoiBat();
        Task<List<SanphamDTO>> SpMoiNhat();
    }
}
