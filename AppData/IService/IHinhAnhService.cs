using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IHinhAnhService
    {
        Task<List<Hinhanh>> GetAll();
        Task<Hinhanh> GetById(int id);
        Task Create(HinhanhDTO dto);
        Task Update(HinhanhDTO dto);
        Task Delete(int id);
    }
}
