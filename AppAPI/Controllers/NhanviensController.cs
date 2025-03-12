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
        private readonly INhanVienService _Service;
        public NhanviensController(INhanVienService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Service.GetAllNhanviensAsync();
            return Ok(result.Select(nv => new
            {
                nv.Hovaten,
                nv.Ngaysinh,
                nv.Diachi,
                Gioitinh = nv.Gioitinh == true ? "Nam" : "Nữ",
                nv.Sdt,
                Trangthai = nv.Trangthai == 0 ? "Hoạt động" : "Dừng hoạt động",
                nv.Password,
                Role = nv.Role == 0 ? "Quản lý" : "Nhân viên"
            }));
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var nhanvien = await _Service.GetNhanvienByIdAsync(id);
                return Ok(new
                {
                    nhanvien.Hovaten,
                    nhanvien.Ngaysinh,
                    nhanvien.Diachi,
                    Gioitinh = nhanvien.Gioitinh == false ? "Nam" : "Nữ",
                    nhanvien.Sdt,
                    Trangthai = nhanvien.Trangthai == 0 ? "Hoạt động" : "Dừng hoạt động",
                    nhanvien.Password,
                    Role = nhanvien.Role == 0 ? "Quản lý" : "Nhân viên"
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Nhân viên không tồn tại.");
            }


        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NhanvienDTO nhanvienDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _Service.AddNhanvienAsync(nhanvienDto);
            return CreatedAtAction(nameof(GetById), new { id = nhanvienDto.Hovaten }, nhanvienDto);
        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NhanvienDTO nhanvienDto)
        {
            try
            {
                await _Service.UpdateNhanvienAsync(id, nhanvienDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Nhân viên không tồn tại.");
            }
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _Service.DeleteNhanvienAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Nhân viên không tồn tại.");
            }
        }
    }
}
