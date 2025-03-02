using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface ISaleRepo
    {
        Task<List<Sale>> GetAll();
        Task<Sale> GetById(int id);
        Task Create(Sale sale);
        Task Update(Sale sale);
        Task Delete(int id);
    }
}
