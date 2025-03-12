﻿using AppData.Models;
using AppData.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepository
{
    public interface IHoaDonChiTietRepo
    {
        Task<IEnumerable<Hoadonchitiet>> GetAllAsync();
        Task<Hoadonchitiet> GetByIdAsync(int id);
        Task AddAsync(Hoadonchitiet entity);
        Task UpdateAsync(Hoadonchitiet entity);
        Task DeleteAsync(int id);
        Task<List<Hoadonchitiet>> HoadonchitietByIDHD(int id);
        Task<List<HoadonchitietViewModel>> HoadonchitietTheoMaHD(int id);
        Task<List<HoadonchitietViewModel>> Checksoluong(int id);
    }
}
