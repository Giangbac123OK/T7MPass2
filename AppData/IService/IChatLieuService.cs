using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IChatLieuService
    {
        Task<List<ChatLieu>> GetAll();
        Task<ChatLieu> GetById(int id);
        Task Create(ChatLieuDTO dto);
        Task Update(ChatLieuDTO dto);
        Task Delete(int id);
    }
}
