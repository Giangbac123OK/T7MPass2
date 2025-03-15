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
    public class SanphamsController : ControllerBase
    {
        private readonly ISanPhamService _service;

        public SanphamsController(ISanPhamService service)
        {
            _service = service;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sanpham = await _service.GetByIdAsync(id);
            return sanpham != null ? Ok(sanpham) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SanphamDTO sanphamDto)
        {
            await _service.AddAsync(sanphamDto);
            return CreatedAtAction(nameof(GetById), new { id = sanphamDto.Idth }, sanphamDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SanphamDTO sanphamDto)
        {
            await _service.UpdateAsync(id, sanphamDto);
            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> UpdateStatusToCancelled(int id)
        {
            try
            {
                await _service.UpdateStatusToCancelled(id);
                return NoContent(); // Thành công mà không cần trả về nội dung
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}/update-status-load")]
        public async Task<IActionResult> UpdateStatusload(int id)
        {
            try
            {
                await _service.UpdateStatusLoad(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName(string name) => Ok(await _service.SearchByNameAsync(name));

        [HttpGet("GetALLSanPham")]
        public async Task<IActionResult> GetAllSanphams()
        {
            try
            {
                var sanphamViewModels = await _service.GetAllSanphamViewModels();
                return Ok(sanphamViewModels);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ chung nếu có lỗi khác
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetALLSanPham/{id}")]
        public async Task<IActionResult> GetAllSanphamsByIdSP(int id)
        {
            try
            {
                var sanphamViewModels = await _service.GetAllSanphamViewModelsByIdSP(id);
                return Ok(sanphamViewModels);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetALLSanPhamGiamGia")]
        public async Task<IActionResult> GetAllSanphamsGiamGia()
        {
            try
            {
                var sanphamViewModels = await _service.GetAllSanphamGiamGiaViewModels();
                return Ok(sanphamViewModels);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ chung nếu có lỗi khác
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetALLSanPhamByThuongHieu/{id}")]
        public async Task<IActionResult> GetAllSanphamsByThuongHieu(int id)
        {
            try
            {
                var sanphamViewModels = await _service.GetAllSanphamByThuongHieu(id);
                return Ok(sanphamViewModels);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("SanPhamChiTiet/search")]
        public async Task<IActionResult> SearchSanphams(
            
            [FromQuery] decimal? giaMin = null,
             [FromQuery] decimal? giaMax = null,
            [FromQuery] int? idThuongHieu = null)
        {
            try
            {
                
                var sanphams = await _service.GetSanphamByThuocTinh(giaMin, giaMax, idThuongHieu);
                if (sanphams == null || !sanphams.Any())
                {
                    return NotFound(new { message = "Không tìm thấy sản phẩm nào thỏa mãn tiêu chí. thanh" });
                }

                return Ok(sanphams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra", error = ex.Message });
            }
        }
    }
}
