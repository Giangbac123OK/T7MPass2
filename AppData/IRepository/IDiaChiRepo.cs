using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IDiaChiRepo
    {
        Task<List<Danhgia>> GetAll();
        Task<Danhgia> GetById(int id);
        Task Create(Danhgia danhgia);
        Task Update(Danhgia danhgia);
        Task Delete(int id);
    }
}
