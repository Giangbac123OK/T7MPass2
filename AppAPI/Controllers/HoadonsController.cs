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

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoadonsController : ControllerBase
    {
        private readonly IHoaDonService _services;

        public HoadonsController(IHoaDonService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hoadons = await _services.GetAll();
            return Ok(hoadons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoadon = await _services.GetById(id);
            if (hoadon == null)
                return NotFound("Hóa đơn không tồn tại.");

            return Ok(hoadon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HoadonDTO dto)
        {
            dto.Id = id;

            try
            {
                await _services.Update(dto);
                return Ok(new { message = "Cập nhật Hóa đơn thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(HoadonDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.Create(dto);
            return Ok(new { message = "Thêm Hóa đơn thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHinhanh = await _services.GetById(id);
            if (existingHinhanh == null)
                return NotFound("Hóa đơn không tồn tại.");

            await _services.Delete(id);
            return Ok(new { message = "Xóa Hóa đơn thành công." });
        }
    }
}
