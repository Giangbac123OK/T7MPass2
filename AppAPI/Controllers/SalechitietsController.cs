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
    public class SalechitietsController : ControllerBase
    {
        private readonly ISaleChiTietService _services;

        public SalechitietsController(ISaleChiTietService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var salechitiets = await _services.GetAll();
            return Ok(salechitiets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var salechitiet = await _services.GetById(id);
            if (salechitiet == null)
                return NotFound("salechitiet không tồn tại.");

            return Ok(salechitiet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SalechitietDTO dto)
        {
            dto.Id = id;

            try
            {
                await _services.Update(dto);
                return Ok(new { message = "Cập nhật thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalechitietDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.Create(dto);
            return Ok(new { message = "Thêm thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var salechitiet = await _services.GetById(id);
            if (salechitiet == null)
                return NotFound(" không tồn tại.");

            await _services.Delete(id);
            return Ok(new { message = "Xóa  thành công." });
        }
    }
}
