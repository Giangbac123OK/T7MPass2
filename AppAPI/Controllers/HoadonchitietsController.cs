using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Models;
using AppData.IService;
using AppData.DTO;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoadonchitietsController : ControllerBase
    {
        private readonly IHoaDonChiTietService _services;

        public HoadonchitietsController(IHoaDonChiTietService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sizes = await _services.GetAll();
            return Ok(sizes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoadonchitiet = await _services.GetById(id);
            if (hoadonchitiet == null)
                return NotFound("Hóa đơn chi tiết không tồn tại.");

            return Ok(hoadonchitiet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HoadonchitietDTO dto)
        {
            dto.Id = id;

            try
            {
                await _services.Update(dto);
                return Ok(new { message = "Cập nhật Hóa đơn chi tiết thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(HoadonchitietDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.Create(dto);
            return Ok(new { message = "Thêm Hóa đơn chi tiết thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHinhanh = await _services.GetById(id);
            if (existingHinhanh == null)
                return NotFound("Hóa đơn chi tiết không tồn tại.");

            await _services.Delete(id);
            return Ok(new { message = "Xóa Hóa đơn chi tiết thành công." });
        }
    }
}
