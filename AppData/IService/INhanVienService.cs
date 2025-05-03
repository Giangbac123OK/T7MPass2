using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface INhanVienService
    {
        Task<IEnumerable<NhanvienDTO>> GetAllNhanviensAsync();
        Task<NhanvienDTO> GetNhanvienByIdAsync(int id);
        Task<int> AddNhanvienAsync(NhanvienDTO nhanvienDto);
        Task UpdateNhanvienAsync(int id, NhanvienUpdateDTO nhanvienDto);
        Task DeleteNhanvienAsync(int id);
        Task<IEnumerable<NhanvienDTO>> TimKiemNhanvienAsync(string search);
        Task UpdateThongTinNhanvienAsync(int id, NhanvienUpdateDTO nhanvienDto);
    }
}
