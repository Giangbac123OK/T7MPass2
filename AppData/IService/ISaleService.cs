using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAll();
        Task<Sale> GetById(int id);
        Task Create(SaleDTO dto);
        Task Update(SaleDTO dto);
        Task Delete(int id);
    }
}
