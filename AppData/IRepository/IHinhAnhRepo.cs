using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IHinhAnhRepo
    {
        Task<List<Hinhanh>> GetAll();
        Task<Hinhanh> GetById(int id);
        Task Create(Hinhanh hinhanh);
        Task Update(Hinhanh hinhanh);
        Task Delete(int id);
    }
}
