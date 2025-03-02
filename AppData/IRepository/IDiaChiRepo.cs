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
        Task<List<Diachi>> GetAll();
        Task<Diachi> GetById(int id);
        Task Create(Diachi diachi);
        Task Update(Diachi diachi);
        Task Delete(int id);
    }
}
