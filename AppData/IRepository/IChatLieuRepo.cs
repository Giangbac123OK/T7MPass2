using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IChatLieuRepo
    {
        Task<List<ChatLieu>> GetAll();
        Task<ChatLieu> GetById(int id);
        Task Create(ChatLieu chatLieu);
        Task Update(ChatLieu chatLieu);
        Task Delete(int id);
    }
}
