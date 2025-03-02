using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Models;
using AppData.DTO;
using AppData.IRepository;
using AppData.IService;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanviensController : ControllerBase
    {
        private readonly INhanVienService _services;

        public NhanviensController(INhanVienService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var nhanviens = await _services.GetAll();
            return Ok(nhanviens);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var nhanvien = await _services.GetById(id);
            if (nhanvien == null)
                return NotFound("Nhân viên không tồn tại.");

            return Ok(nhanvien);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NhanvienDTO dto)
        {
            dto.Id = id;

            try
            {
                await _services.Update(dto);
                return Ok(new { message = "Cập nhật nhân viên thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(NhanvienDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.Create(dto);
            return Ok(new { message = "Thêm nhân viên thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var nhanvien = await _services.GetById(id);
            if (nhanvien == null)
                return NotFound("Nhân viên không tồn tại.");

            await _services.Delete(id);
            return Ok(new { message = "Xóa nhân viên thành công." });
        }
    }
}
