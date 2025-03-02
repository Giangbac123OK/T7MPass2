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
using AppData.IService;
using AppData.IRepository;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachhangsController : ControllerBase
    {
        private readonly IKhachHangService _services;

        public KhachhangsController(IKhachHangService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var khachhangs = await _services.GetAll();
            return Ok(khachhangs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoadonchitiet = await _services.GetById(id);
            if (hoadonchitiet == null)
                return NotFound("Khách hàng không tồn tại.");

            return Ok(hoadonchitiet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, KhachhangDTO dto)
        {
            dto.Id = id;

            try
            {
                await _services.Update(dto);
                return Ok(new { message = "Cập nhật khách hàng thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(KhachhangDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.Create(dto);
            return Ok(new { message = "Thêm khách hàng thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHinhanh = await _services.GetById(id);
            if (existingHinhanh == null)
                return NotFound("Khách hàng không tồn tại.");

            await _services.Delete(id);
            return Ok(new { message = "Xóa khách hàng thành công." });
        }
    }
}
