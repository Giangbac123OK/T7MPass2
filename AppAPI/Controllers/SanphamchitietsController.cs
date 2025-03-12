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
    public class SanphamchitietsController : ControllerBase
    {
        private readonly ISanPhamChiTietService _service;
        public SanphamchitietsController(ISanPhamChiTietService service)
        {
            _service = service;

        }
        // API để lấy tất cả sản phẩm chi tiết
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var SanPhamCTList = await _service.GetAllAsync();
            return Ok(SanPhamCTList);
        }

        // API để lấy sản phẩm chi tiết theo Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var SanPhamCT = await _service.GetByIdAsync(id);
            if (SanPhamCT == null) return NotFound(new { message = "Sản phẩm chi tiết không tìm thấy" });
            return Ok(SanPhamCT);
        }

        [HttpGet("sanpham/{id}")]
        public async Task<IActionResult> GetByIdSPAsync(int id)
        {
            try
            {
                var thuoctinhDto = await _service.GetByIdSPAsync(id);

                if (thuoctinhDto == null || !thuoctinhDto.Any())
                {
                    return NotFound(new { Message = "Không tìm thấy sản phẩm trong sản phẩm chi tiết với ID: " + id });
                }

                return Ok(thuoctinhDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(SanphamchitietDTO dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Mota }, dto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SanphamchitietDTO dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
