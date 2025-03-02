using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface ISaleChiTietRepo
    {
        Task<List<Salechitiet>> GetAll();
        Task<Salechitiet> GetById(int id);
        Task Create(Salechitiet salechitiet);
        Task Update(Salechitiet salechitiet);
        Task Delete(int id);
    }
}
