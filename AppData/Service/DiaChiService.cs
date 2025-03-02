using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using AppData.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Service
{
    public class DiaChiService : IDiaChiService
    {
        private readonly IDiaChiRepo _repository;
        private readonly IKhachHangRepo _KHrepository;
        public DiaChiService(IDiaChiRepo repository, IKhachHangRepo kHrepository)
        {
            _repository = repository;
            _KHrepository = kHrepository;
        }

        public async Task Create(DiachiDTO dto)
        {
            var khachhang = await _KHrepository.GetById(dto.Idkh);
            if (khachhang == null) return;

            var diachi = new Diachi
            {
                Idkh = dto.Idkh,
                Tennguoinhan = dto.Tennguoinhan,
                sdtnguoinhan = dto.sdtnguoinhan,
                Thanhpho = dto.Thanhpho,
                Quanhuyen = dto.Quanhuyen,
                Phuongxa = dto.Phuongxa,
                Diachicuthe = dto.Diachicuthe
            };

            await _repository.Create(diachi);
        }

        public async Task Delete(int id) => await _repository.Delete(id);

        public async Task<List<Diachi>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Diachi> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Update(DiachiDTO dto)
        {
            var diachi = await _repository.GetById(dto.Id);
            if (diachi == null) return;

            var khachhang = await _KHrepository.GetById(dto.Idkh);
            if (khachhang == null) return;

            diachi.Idkh = dto.Idkh;
            diachi.Tennguoinhan = dto.Tennguoinhan;
            diachi.sdtnguoinhan = dto.sdtnguoinhan;
            diachi.Thanhpho = dto.Thanhpho;
            diachi.Quanhuyen = dto.Quanhuyen;
            diachi.Phuongxa = dto.Phuongxa;
            diachi.Diachicuthe = dto.Diachicuthe;

            await _repository.Update(diachi);
        }
    }
}
