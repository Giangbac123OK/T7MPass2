﻿using AppData.DTO;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IService
{
    public interface IThongkeService
    {
        Task<List<TopSellingProductDTO>> GetTopSellingProductsByTimeAsync(DateTime startDate, DateTime endDate);
        Task<List<CustomerStatisticsDTO>> GetTop5CustomersAsync(DateTime month);
        Task<List<Khachhang>> GetActiveCustomersAsync();
    }
}
